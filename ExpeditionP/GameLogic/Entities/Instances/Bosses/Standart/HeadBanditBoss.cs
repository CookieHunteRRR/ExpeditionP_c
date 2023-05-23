using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities.Instances.Bosses.Standart
{
    internal class HeadBanditBoss : Boss
    {
        static readonly double escapeChance = 1;
        static readonly string attackMessage = "Бандит бьет вас, нанося {0} урона";
        static readonly string doubleAttackMessage = "Бандит бьет вас вновь, нанося {0} урона";

        internal HeadBanditBoss() : base("Главарь бандитов", Tag.Standart)
        {
            Info.SetRandomAppearance(false);
            //IsReactingToEvents = true;

            //BasicAttack = new Attack_HeadBandit();
            //AddTrigger(0, new Ability_HeadBandit_Reinforcements());

            // Добавление в пул атак
            AI.AddAttackToPool(new Attack_HeadBandit());
            AI.AddAttackToPool(new AttackList.Attack_Bandit());
            // Добавление в пул абилок
            AI.AddAbilityToPool(new Ability_HeadBandit_Reinforcements()); // Абилка-триггер
            // Регистрация поведений
            // Поведение босса
            var bossBehavior = new EntityBehavior();
            bossBehavior.AttackIndexes.Add(0);
            bossBehavior.AbilityIndexes.Add(0);
            AI.AddBehavior(bossBehavior);
            // Поведение бандита
            var banditBehavior = new EntityBehavior();
            banditBehavior.AttackIndexes.Add(1);
            AI.AddBehavior(banditBehavior);

            Stats.Health = 200;
            Stats.Defense = 15;

            LootTable.AddItem(ItemHolder.RegisteredItems["acc_headhunter"]);
        }

        /*internal override void RegisterEvents()
        {
            EventManager.MobDeathEvent.UserEvent += this.onMobDeath;
        }

        internal override void UnregisterEvents()
        {
            EventManager.MobDeathEvent.UserEvent -= this.onMobDeath;
        }

        internal void onMobDeath(ExpeditionManager manager)
        {
            EventManager.MobDeathEvent.IsCancelled = true;
            UnregisterEvents(); // т.к. триггер происходит единожды
        }*/

        class Attack_HeadBandit : Attack
        {
            static readonly double doubleAttackChance = 0.3;

            internal Attack_HeadBandit()
            {
                MinDamage = 10;
                MaxDamage = 12;
                DamageType = DamageType.Physical;
                Message = attackMessage;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                if (Utils.CheckProbability(doubleAttackChance))
                {
                    battle.MakeAttack(this, false);
                    Message = doubleAttackMessage;
                }
                battle.MakeAttack(this, false);
                Message = attackMessage;
            }
        }

        class Ability_HeadBandit_Reinforcements : Ability
        {
            internal Ability_HeadBandit_Reinforcements() : base(-1, 0)
            {
                IsReactingToEvents = true;
                UsedDirectly = false;
            }

            internal override void RegisterEvents()
            {
                EventManager.MobDeathEvent.UserEvent += this.onMobDeath;
            }

            internal override void UnregisterEvents()
            {
                EventManager.MobDeathEvent.UserEvent -= this.onMobDeath;
            }

            internal void onMobDeath(ExpeditionManager manager)
            {
                EventManager.MobDeathEvent.IsCancelled = true;
                UnregisterEvents(); // т.к. триггер происходит единожды

                var ai = manager.BattleManager.Enemy.AI;
                // Находим индекс этого триггера
                var result = ai.FindAbilityIndex(this);
                if (result < 0)
                {
                    throw new Exception("Не удалось найти индекс способности");
                }
                var action = new Action(ActionType.Ability, result);
                ai.QueuedActions.Enqueue(action);
            }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                // Проверяем, произойдет ли вообще этот триггер
                if (Utils.CheckProbability(escapeChance))
                {
                    base.Activate(manager, caster);

                    manager.SendToLog("Главарь бандитов в спешке сбегает с поля боя, вызывая подкрепление");

                    // Сбрасываем все что было на боссе
                    Boss boss = (Boss)caster;
                    boss.BattleStats.CurrentEffects.Clear();
                    boss.Triggers.Clear();

                    // По сути меняем босса на моба
                    var bandit = (Mob)EntityHolder.RegisteredEntities["mob_standart_bandit"];
                    boss.Info.Name = bandit.GetName();
                    boss.Stats.SetStatsTo(bandit.Stats);
                    boss.RecalculateStats();
                    boss.AI.ChangeBehavior(1);
                    boss.BattleStats.HiddenEntityStats = bandit.BattleStats.HiddenEntityStats;
                    boss.BattleStats.CurrentHealth = boss.BattleStats.CurrentEntityStats.Health;
                    boss.BattleStats.CurrentMana = boss.BattleStats.CurrentEntityStats.Mana;
                    /*var bandit = ((Mob)Program.Game.GameManager.EntityHolder.RegisteredEntities["mob_standart_bandit"]).Copy();
                    boss.Info.Name = bandit.GetName();
                    boss.Stats.SetStatsTo(bandit.Stats);
                    boss.RecalculateStats();
                    boss.BasicAttack = bandit.BasicAttack;
                    boss.BattleStats.HiddenEntityStats = bandit.BattleStats.HiddenEntityStats;
                    boss.BattleStats.CurrentHealth = boss.BattleStats.CurrentEntityStats.Health;
                    boss.BattleStats.CurrentMana = boss.BattleStats.CurrentEntityStats.Mana;*/
                }
            }
        }
    }
}
