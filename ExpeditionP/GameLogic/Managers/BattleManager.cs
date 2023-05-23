using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Managers
{
    /// <summary>
    /// Отвечает за бой во время экспедиции
    /// </summary>
    internal class BattleManager
    {
        ExpeditionManager ExpeditionManager { get; init; }
        internal Player Player { get; init; } // Оригинальный Entity используется для оригинальных статов
        internal Mob OriginalMob { get; init; }
        internal Mob Enemy { get; init; }

        internal bool wasWeaponEquippedThisTurn = false;
        internal bool wasWeaponUnequippedThisTurn = false;
        internal bool wasFleeAttempted = false;
        internal int TurnCount { get; private set; } = 0;

        internal BattleManager(ExpeditionManager manager, string enemyId)
        {
            ExpeditionManager = manager;
            Player = ExpeditionManager.GameInstance.Player;
            OriginalMob = (Mob)EntityHolder.RegisteredEntities[enemyId];
            Enemy = OriginalMob.Copy();
            Enemy.Stats.Health = Utils.Round(Enemy.Stats.Health * ExpeditionManager.DifficultyModifier);
            Enemy.RecalculateStats();

            // регистрируем моба
            RegisterListeners();
        }

        /// <summary>
        /// Ставить в любое место (?) где потенциально может закончиться игра
        /// </summary>
        /// <returns></returns>
        bool IsExpeditionValid()
        {
            if (ExpeditionManager is null) return false;
            if (ExpeditionManager.BattleManager is null) return false;
            return true;
        }

        void RegisterListeners()
        {
            // Регистрируем листенеры моба
            ExpeditionManager.RegisterEventSubject(OriginalMob);
            // Регистрируем листенеры абилок моба
            Enemy.AI.RegisterAbilities();
            // foreach ability
        }

        void UnregisterListeners()
        {
            ExpeditionManager.UnregisterEventSubject(OriginalMob);
            Enemy.AI.UnregisterAbilities();
            // foreach ability
        }

        internal BattleStats GetEntityBattleStats(Entity entity) { return entity.BattleStats; }

        internal void MakeEnemyTurn()
        {
            if (!IsExpeditionValid()) return; // В начале боя/после хода игрока

            // Планируем новое действие если это не самое начало боя
            if (TurnCount > 0) Enemy.AI.DecideNextAction();
            // Исполняем все запланированные действия
            for (int i = 0; i < Enemy.AI.QueuedActions.Count; i++) // я не знаю если каунт уменьшаться будет не поломается ли что то
            {
                var action = Enemy.AI.QueuedActions.Dequeue();
                ExecuteEnemyAction(action);
                if (!IsExpeditionValid()) return;
            }

            TurnCount++;
            ActivatePostTurnEffects();
            if (!IsExpeditionValid()) return;

            ReduceEffectDurations();
            ReduceCooldowns();

            ExpeditionManager.ExpeditionForm.UpdateAll();

            ExpeditionManager.ExpeditionForm.SetBattleControlsVisibility(true);
            ExpeditionManager.ExpeditionForm.SetBattleControlsAvailability(true);
            ExpeditionManager.ExpeditionForm.SetFleeBtnAvailability(!wasFleeAttempted);

            wasWeaponEquippedThisTurn = false;
            wasWeaponUnequippedThisTurn = false;
        }

        void ExecuteEnemyAction(Action action)
        {
            switch (action.Type)
            {
                // Активирует рандомную абилку (ладно не совсем рандомную) не на кулдауне
                case ActionType.Ability:
                    // value - индекс в AbilityList
                    var ability = Enemy.AI.GetAbilityByIndex(action.Value);
                    ability.Activate(ExpeditionManager, Enemy);

                    break;
                case ActionType.Attack:
                    // value - индекс в AttackList
                    var attack = Enemy.AI.GetAttackByIndex(action.Value);
                    attack.Hit(ExpeditionManager);

                    break;
            }
            // Очень плохая реализация сброса отмены ивента, т.к. на данный момент она работает только лишь для Главаря бандитов
            // Если в будущем что-то потребует отмену этого ивента то могут получиться конфликты
            EventManager.MobDeathEvent.IsCancelled = false;
            CheckHealth();
        }

        internal void MakePlayerTurn(Action playerAction)
        {
            int doesntUseTurn = 0;
            if (playerAction.SpecialValues.TryGetValue("doesntUseTurn", out doesntUseTurn)) // Если такой тег имеется в спец. значениях
            {
                if (doesntUseTurn > 0) // Если действие не тратит ход
                {
                    ExecutePlayerAction(playerAction);
                    return;
                }
            }
            // Если тега нет (т.е. считаем дефолтное значение - ход тратится) либо doesntUseTurn = 0

            ActivatePreTurnEffects(); // т.к. по сути это начало хода
            if (!IsExpeditionValid()) return;
            ExpeditionManager.ExpeditionForm.SetBattleControlsAvailability(false);
            ExpeditionManager.ExpeditionForm.SetFleeBtnAvailability(false);

            ExecutePlayerAction(playerAction);
            if (!IsExpeditionValid()) return;

            CheckHealth();
            MakeEnemyTurn();
        }

        void ExecutePlayerAction(Action action)
        {
            switch (action.Type)
            {
                case ActionType.Attack:
                    Player.CurrentEquippedWeapon.Attack.Hit(ExpeditionManager);
                    break;
                case ActionType.FleeAttempt:
                    AttemptToFlee();
                    break;
            }
        }

        void EndBattle(BattleFinishReason battleFinishReason)
        {
            wasWeaponUnequippedThisTurn = false;
            wasWeaponEquippedThisTurn = false;

            EventManager.BattleEndEvent.Invoke(ExpeditionManager, new BattleEndEventArgs(battleFinishReason));

            // Отписываем всех от ивентов требующих активный бой
            UnregisterListeners();

            switch (battleFinishReason)
            {
                case BattleFinishReason.EnemyDied:
                    // Метод в котором игроку предлагается выбрать предмет из дропа
                    List<Item> drops = ExpeditionManager.ItemGenerator.GenerateLoot(true, Enemy.LootTable);
                    if (drops.Count > 0)
                    {
                        ExpeditionManager.ExpeditionForm.SetMoveBtnAvailability(false);
                        ExpeditionManager.Form_ItemPick.LoadForm(drops);
                    }
                    else ExpeditionManager.ExpeditionForm.SetMoveBtnAvailability(true);

                    List<Effect> effectsToRemove = new List<Effect>();
                    foreach (var effect in Player.BattleStats.CurrentEffects)
                    {
                        if (effect.IsRemovedOnBattleEnd) effectsToRemove.Add(effect);
                    }
                    foreach (var effect in effectsToRemove)
                    {
                        Player.BattleStats.UndoEffect(effect);
                    }
                    break;
                case BattleFinishReason.PlayerFled:
                    ExpeditionManager.ExpeditionForm.SetMoveBtnAvailability(true);
                    break;
                case BattleFinishReason.PlayerDied:
                    // Очистить инвентарь игрока и сброс до дефолтных статов
                    // Выброс в хайдаут и сброс экспедишн менеджера в QuitBattle()
                    break;
            }
            ExpeditionManager.ExpeditionForm.QuitBattle(battleFinishReason);
        }

        // Проверяет, достаточно ли осталось у сражающихся здоровья, чтобы продолжать бой
        internal void CheckHealth()
        {
            if (Enemy.BattleStats.CurrentHealth <= 0)
            {
                Enemy.BattleStats.CurrentHealth = 0;
                ExpeditionManager.ExpeditionForm.UpdateEnemyInfo();
            }
            if (Player.BattleStats.CurrentHealth <= 0)
            {
                Player.BattleStats.CurrentHealth = 0;
                ExpeditionManager.ExpeditionForm.UpdatePlayerStats();
            }

            // Приоритетнее смерть игрока
            if (Player.BattleStats.CurrentHealth == 0)
            {
                EndBattle(BattleFinishReason.PlayerDied);
            }
            else if (Enemy.BattleStats.CurrentHealth == 0)
            {
                EventManager.MobDeathEvent.Invoke(ExpeditionManager);
                if (!EventManager.MobDeathEvent.IsCancelled)
                    EndBattle(BattleFinishReason.EnemyDied);
            }
        }

        internal AttackInstance MakeAttack(Attack attack, bool isPlayerAttacking)
        {
            if (isPlayerAttacking)
            {
                AttackInstance attackInstance = CalculateDamage(attack, Player.BattleStats, Enemy.BattleStats);
                Enemy.ChangeCurrentHealth(-attackInstance.Damage, ExpeditionManager);
                if (Player.BattleStats.HiddenEntityStats.Vampirism > 0 && !Enemy.EntityTags.Contains(Tag.Bloodless))
                {
                    double vampiricPercent = (double)Player.BattleStats.HiddenEntityStats.Vampirism / 100;
                    int vampiricHeal = Utils.Round(attackInstance.Damage * vampiricPercent);
                    Player.ChangeCurrentHealth(vampiricHeal, ExpeditionManager);
                    AddVampiricInfo(attackInstance, vampiricHeal);
                }
                ExpeditionManager.SendToLog(String.Format(attack.Message, attackInstance.Damage) + " " + attackInstance.AdditionalString);
                EventManager.PlayerAttackEvent.Invoke(ExpeditionManager, new PlayerAttackEventArgs(!attackInstance.IsEvaded, attackInstance.IsCrit));
                return attackInstance;
            }
            else
            {
                AttackInstance attackInstance = CalculateDamage(attack, Enemy.BattleStats, Player.BattleStats);
                Player.ChangeCurrentHealth(-attackInstance.Damage, ExpeditionManager);
                if (Enemy.BattleStats.HiddenEntityStats.Vampirism > 0 && !Player.EntityTags.Contains(Tag.Bloodless))
                {
                    double vampiricPercent = (double)Enemy.BattleStats.HiddenEntityStats.Vampirism / 100;
                    int vampiricHeal = Utils.Round(attackInstance.Damage * vampiricPercent);
                    Enemy.ChangeCurrentHealth(vampiricHeal, ExpeditionManager);
                    AddVampiricInfo(attackInstance, vampiricHeal);
                }
                ExpeditionManager.SendToLog(String.Format(attack.Message, attackInstance.Damage) + " " + attackInstance.AdditionalString);
                EventManager.MobAttackEvent.Invoke(ExpeditionManager, new MobAttackEventArgs(!attackInstance.IsEvaded, attackInstance.IsCrit));
                return attackInstance;
            }
        }

        // Снижает длительность эффектов всех бойцов в конце хода
        void ReduceEffectDurations()
        {
            ExpeditionManager.ReducePlayerEffectDurations();

            if (Enemy.BattleStats.CurrentEffects.Count > 0)
            {
                List<Effect> effectsToRemove = new List<Effect>();
                foreach (var effect in Enemy.BattleStats.CurrentEffects)
                {
                    effect.TurnsLeft--;
                    if (effect.TurnsLeft <= 0)
                    {
                        effectsToRemove.Add(effect);
                    }
                }
                foreach (var effect in effectsToRemove)
                {
                    Enemy.BattleStats.UndoEffect(effect);
                }
            }
        }

        // Снижение кулдаунов в конце хода
        void ReduceCooldowns()
        {
            // Срезаем кд для игрока
            //foreach (var item in Player.EquippedAccessories) для аксессуаров абилки позже
            //foreach (var ability in Player.Abilities) тоже потом
            ExpeditionManager.ReducePlayerCooldowns();
            // Срезаем кд для мобов
            Enemy.AI.ReduceAbilityCooldowns();
        }

        void AttemptToFlee()
        {
            wasFleeAttempted = true;
            var args = new FleeAttemptEventArgs();
            EventManager.FleeAttemptEvent.Invoke(ExpeditionManager, args);
            if (!args.IsCancelled)
            {
                double playerChance = Player.FleeSuccessChance + (Player.BattleStats.HiddenEntityStats.FleeChance / 100d);
                double chanceToFlee = playerChance - (Enemy.FleeInterruptChance * ExpeditionManager.DifficultyModifier);
                if (Utils.CheckProbability(chanceToFlee))
                {
                    EndBattle(BattleFinishReason.PlayerFled);
                }
                else ExpeditionManager.SendToLog("Вам не удалось сбежать от противника");
            }
        }

        void ActivatePostTurnEffects()
        {
            foreach (Effect effect in Player.BattleStats.CurrentEffects)
            {
                if (effect.ActivationType == Items.ActivationType.PostTurn)
                {
                    effect.ApplySpecialEffect(ExpeditionManager, Player);
                    if (!IsExpeditionValid()) return;
                }
            }
            foreach (Effect effect in Enemy.BattleStats.CurrentEffects)
            {
                if (effect.ActivationType == Items.ActivationType.PostTurn)
                {
                    effect.ApplySpecialEffect(ExpeditionManager, Enemy);
                    if (!IsExpeditionValid()) return;
                }
            }

            // хил тоже пожалуй здесь будет
            if (Player.BattleStats.HiddenEntityStats.Heal != 0)
                Player.ChangeCurrentHealth(Player.BattleStats.HiddenEntityStats.Heal, ExpeditionManager);
            if (Enemy.BattleStats.HiddenEntityStats.Heal != 0)
                Enemy.ChangeCurrentHealth(Enemy.BattleStats.HiddenEntityStats.Heal, ExpeditionManager);

            //CheckHealth();
            ExpeditionManager.ExpeditionForm.UpdateAll();
        }

        void ActivatePreTurnEffects()
        {
            foreach (Effect effect in Player.BattleStats.CurrentEffects)
            {
                if (effect.ActivationType == Items.ActivationType.PreTurn)
                {
                    effect.ApplySpecialEffect(ExpeditionManager, Player);
                    if (!IsExpeditionValid()) return;
                }
            }
            foreach (Effect effect in Enemy.BattleStats.CurrentEffects)
            {
                if (effect.ActivationType == Items.ActivationType.PreTurn)
                {
                    effect.ApplySpecialEffect(ExpeditionManager, Enemy);
                    if (!IsExpeditionValid()) return;
                }
            }
            ExpeditionManager.ExpeditionForm.UpdateAll();
        }

        void AddVampiricInfo(AttackInstance attack, int vampValue)
        {
            if (attack.AdditionalString.Length > 0)
            {
                attack.AdditionalString = attack.AdditionalString.Insert(attack.AdditionalString.Length - 1,
                        ", восстановлено " + vampValue + " здоровья");
            }
            else
            {
                attack.AdditionalString = "(восстановлено " + vampValue + " здоровья)";
            }
        }

        AttackInstance CalculateDamage(Attack attack, BattleStats attackerStats, BattleStats enemyStats)
        {
            double damageDealt = attack.GetDamage();
            EntityStats attSt = attackerStats.CurrentEntityStats; // Attacker Stats
            HiddenStats attHSt = attackerStats.HiddenEntityStats;
            EntityStats eneSt = enemyStats.CurrentEntityStats; // Enemy Stats
            HiddenStats eneHSt = enemyStats.HiddenEntityStats;

            AttackInstance atkinst;
            switch (attack.DamageType)
            {
                case DamageType.Physical:
                    // влияет кд и кш аттакера, эвейжен и дефенс врага
                    // Сначала проверяем не уклонился ли вражина
                    if (attack.CanBeEvaded)
                    {
                        if (Utils.CheckProbability((double)eneSt.Evasion / 100))
                        {
                            atkinst = new AttackInstance(0, String.Format("({0})", Constants.evasionSuccessfulMessage));
                            atkinst.SetDebugInfo(true, false, attack.GetType().Name);
                            return atkinst;
                        }
                    }
                    double isCrit = CalculateCritChance(attackerStats, enemyStats);
                    double critMultiplier = 1 + ((double)attSt.CritDamage / 100 * isCrit);
                    double defenseMultiplier = Math.Min(Math.Max(Constants.minimalDefenseMultiplier, 1 - ((double)eneSt.Defense / 100)), Constants.maximumDefenseMultiplier);
                    string additionalInfo = String.Empty;
                    if (isCrit > 0) additionalInfo = String.Format("({0})", Constants.critSuccessfulMessage);
                    int physDmgBonus = CalculatePhysDmgBonus(attackerStats, enemyStats);
                    damageDealt = (damageDealt + physDmgBonus) * critMultiplier * defenseMultiplier * attHSt.PhysDmgMultiplier * attHSt.AllDmgMultiplier * attHSt.FinalDmgMultiplier;
                    atkinst = new AttackInstance(Utils.Round(damageDealt), additionalInfo);
                    atkinst.SetDebugInfo(false, isCrit, attack.GetType().Name);
                    return atkinst;
                case DamageType.Magical:
                    if (Utils.CheckProbability((double)eneSt.Annulment / 100))
                    {
                        atkinst = new AttackInstance(0, String.Format("({0})", Constants.evasionSuccessfulMessage));
                        atkinst.SetDebugInfo(true, false, attack.GetType().Name);
                        return atkinst;
                    }
                    double mitigationMultiplier = Math.Min(Math.Max(Constants.minimalDefenseMultiplier, 1 - ((double)eneSt.Mitigation / 100)), Constants.maximumDefenseMultiplier);
                    double amplificationMultiplier = 1 + ((double)attSt.Amplification / 100);
                    damageDealt = damageDealt * amplificationMultiplier * mitigationMultiplier * attHSt.AllDmgMultiplier * attHSt.FinalDmgMultiplier;

                    atkinst = new AttackInstance(Utils.Round(damageDealt), String.Empty);
                    atkinst.SetDebugInfo(false, false, attack.GetType().Name);
                    return atkinst;
                case DamageType.Pure:
                    break;
            }
            atkinst = new AttackInstance(Utils.Round(damageDealt), String.Empty);
            atkinst.SetDebugInfo(false, false, attack.GetType().Name);
            return atkinst;
        }

        double CalculateCritChance(BattleStats attackerStats, BattleStats enemyStats)
        {
            if (attackerStats.HiddenEntityStats.GuaranteedCrit) return 1;
            return Utils.CheckProbability((double)attackerStats.CurrentEntityStats.CritChance / 100) ? 1 : 0;
        }

        int CalculatePhysDmgBonus(BattleStats attackerStats, BattleStats enemyStats)
        {
            int toReturn = attackerStats.HiddenEntityStats.PhysDmgBonus;
            // я не знаю как отправить просто тип, поэтому я создаю лишний объект и так отправляю тип
            var deepWoundEffect = enemyStats.FindEffectOfType(new EffectList.Effect_Debuff_DeepWound().GetType());
            if (deepWoundEffect is not null)
                toReturn += deepWoundEffect.Power * 5;
            return toReturn;
        }
    }
}
