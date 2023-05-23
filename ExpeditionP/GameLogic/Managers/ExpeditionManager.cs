using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Items.Instances.Accessories;
using ExpeditionP.GameLogic.Maps;
using ExpeditionP.GameLogic.Maps.Nodes;
using ExpeditionP.SecondaryForms.Expedition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Managers
{
    internal class ExpeditionManager
    {
        internal Form_Expedition ExpeditionForm { get; init; }
        internal Form_ReplaceItem Form_ReplaceItem { get; init; }
        internal Form_ItemPick Form_ItemPick { get; init; }
        internal Form_RestPick Form_RestPick { get; init; }
        internal Form_ExpeditionResult Form_ExpeditionResult { get; init; }

        internal Map CurrentMap { get; init; }
        internal MapNode CurrentNode { get; set; }
        internal double DifficultyModifier { get; set; }

        internal GameInstance GameInstance { get; init; }
        internal ItemGenerator ItemGenerator { get; init; }
        internal BattleManager? BattleManager { get; set; }

        internal ExpeditionManager(Form_Expedition exp, Map map, GameInstance gameInstance)
        {
            ExpeditionForm = exp;
            Form_ReplaceItem = new Form_ReplaceItem(this);
            Form_ItemPick = new Form_ItemPick(this);
            Form_RestPick = new Form_RestPick(this);
            Form_ExpeditionResult = new Form_ExpeditionResult();

            CurrentMap = map;
            CurrentNode = CurrentMap.StartingNodes[0];
            DifficultyModifier = CurrentMap.DifficultySettings.DefaultDifficultyMultiplier;
            GameInstance = gameInstance;
            ItemGenerator = new ItemGenerator(this);

            RegisterListeners();
        }

        internal void SendToLog(string message) { ExpeditionForm.SendToLog(message); }

        internal List<Item> GenerateItemsForItemNode()
        {
            return ItemGenerator.GenerateLoot(false, CurrentMap.LootTable);
        }

        void RegisterListeners()
        {
            foreach (var weapon in GameInstance.Player.EquippedWeapons)
            {
                RegisterEventSubject(weapon);
            }
            foreach (var acc in GameInstance.Player.EquippedAccessories)
            {
                RegisterEventSubject(acc);
            }
        }

        internal void RegisterEventSubject(EventSubject subject)
        {
            if (subject.IsReactingToEvents) subject.RegisterEvents();
        }

        void UnregisterListeners()
        {
            foreach (var weapon in GameInstance.Player.EquippedWeapons)
            {
                UnregisterEventSubject(weapon);
            }
            foreach (var acc in GameInstance.Player.EquippedAccessories)
            {
                UnregisterEventSubject(acc);
            }
        }

        internal void UnregisterEventSubject(EventSubject subject)
        {
            if (subject.IsReactingToEvents) subject.UnregisterEvents();
        }

        internal string GenerateMobId(Tag expeditionTag, bool isBoss)
        {
            var entityTable = EntityHolder.EntityTable;
            DataView filteredItems = new DataView(entityTable);

            List<int> intermediateWeight = new List<int>();
            int totalWeight = 0;
            string entityType = (isBoss) ? "Boss" : "Mob";
            // Фильтр по множителю сложности
            string diffMpFilter = $"(mindiffmp <= {DifficultyModifier.ToString(CultureInfo.InvariantCulture)} AND" +
                $" maxdiffmp >= {DifficultyModifier.ToString(CultureInfo.InvariantCulture)})";
            // Отфильтровали подходящих для карты мобов
            filteredItems.RowFilter = String.Format("entitytype = '{0}' AND (expedition = '{1}' OR expedition = 'Other') AND isappearingrandomly = true AND {2}", 
                entityType, expeditionTag.ToString(), diffMpFilter);
            // Находим весы мобов
            var filteredTable = filteredItems.ToTable();
            foreach (DataRow row in filteredTable.Rows)
            {
                int weight = Int32.Parse(row["weight"].ToString());
                totalWeight += weight;
                intermediateWeight.Add(totalWeight);
            }
            // Сгенерировали рандомный вес
            double generatedWeight = Program.Random.NextDouble() * totalWeight;
            // Нашли индекс соответствующей шмотки
            string generatedMob = String.Empty;
            for (int i = 0; i < intermediateWeight.Count; i++)
            {
                if (generatedWeight < intermediateWeight[i])
                {
                    generatedMob = filteredTable.Rows[i][0].ToString();
                    break;
                }
            }
            return generatedMob;
        }

        internal void QuitExpedition(bool isFinished)
        {
            Form_ReplaceItem.Hide();
            Form_ItemPick.Hide();

            //CurrentMap = null;
            UnregisterListeners();
            CurrentNode = null;
            BattleManager = null;

            // Зачислить 1 прохождение если экспедиция завершена успешно
            // Дать игроку забрать шмотку если успешно
            if (isFinished)
            {
                if (Form_ExpeditionResult.PlayerHasPickableItems(GameInstance.Player))
                {
                    Form_ExpeditionResult.LoadForm(this);
                }
                else
                {
                    MessageBox.Show("Нет предметов, которые можно забрать", "-", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExpeditionForm.ExitExpedition();
                }
            }
            else
            {
                ExpeditionForm.ExitExpedition();
            }
        }

        internal void IncreaseDifficulty()
        {
            var difficultySettings = CurrentMap.DifficultySettings;
            DifficultyModifier += difficultySettings.DifficultyStep;
            if (DifficultyModifier > difficultySettings.MultiplierCap)
                DifficultyModifier = difficultySettings.MultiplierCap;
        }

        internal void ReducePlayerEffectDurations()
        {
            if (GameInstance.Player.BattleStats.CurrentEffects.Count > 0)
            {
                List<Effect> effectsToRemove = new List<Effect>();
                foreach (var effect in GameInstance.Player.BattleStats.CurrentEffects)
                {
                    effect.TurnsLeft--;
                    if (effect.TurnsLeft <= 0)
                    {
                        effectsToRemove.Add(effect);
                    }
                }
                foreach (var effect in effectsToRemove)
                {
                    GameInstance.Player.BattleStats.UndoEffect(effect);
                }
            }
        }

        internal void ReducePlayerCooldowns()
        {
            Player player = GameInstance.Player;
            foreach (var acc in player.EquippedAccessories)
            {
                if (acc is UsableAccessory) ((UsableAccessory)acc).Ability.ReduceCooldown();
            }
        }
    }
}
