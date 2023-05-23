using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    internal class Entity : EventSubject
    {
        internal EntityStats Stats { get; set; }
        internal BattleStats BattleStats { get; set; }
        internal List<Tag> EntityTags { get; set; }
        internal double FleeSuccessChance { get; set; } // пусть у всех энтити шанс побега будет, хоть юзать его будет только игрок (пока что)

        internal Entity()
        {
            //Name = "default entity name";
            Stats = new EntityStats();
            BattleStats = new BattleStats(this, Stats);
            FleeSuccessChance = Constants.defaultFleeSuccessChance; // дефолтное значение
            EntityTags = new List<Tag>();
            Stats.SetStatsToEntityDefault();
        }

        internal virtual string GetName()
        {
            return "default entity name";
        }

        internal override Entity Copy()
        {
            Entity copy = new Entity();
            CopyData(copy);
            return copy;
        }

        protected override void CopyData(object copy)
        {
            Entity correctedCopy = (Entity)copy;
            base.CopyData(correctedCopy);
            correctedCopy.Stats = Stats.Copy();
            correctedCopy.BattleStats = new BattleStats(correctedCopy, correctedCopy.Stats);
            correctedCopy.EntityTags = EntityTags;
            correctedCopy.FleeSuccessChance = FleeSuccessChance;
        }

        internal double GetHpRatio() { return (double)BattleStats.CurrentHealth / (double)BattleStats.CurrentEntityStats.Health; }

        internal void ChangeCurrentHealth(int amount, ExpeditionManager? manager = null)
        {
            if (amount == 0) return;
            BattleStats.CurrentHealth += amount;
            // Проверка на оверхил
            if (BattleStats.CurrentHealth > BattleStats.CurrentEntityStats.Health)
                BattleStats.CurrentHealth = BattleStats.CurrentEntityStats.Health;

            if (manager != null)
            {
                bool isDamaged = (amount > 0) ? false : true;
                EventManager.EntityHealthChangeEvent.Invoke(manager, new EntityHealthChangeEventArgs(this is Player, isDamaged));
            }
        }

        internal virtual void RecalculateStats()
        {
            int prevMaxHealth = BattleStats.CurrentEntityStats.Health;
            int prevMaxMana = BattleStats.CurrentEntityStats.Mana;

            BattleStats.CurrentEntityStats.SetStatsTo(Stats);
            BattleStats.HiddenEntityStats.ResetStats();

            if (this as Player is not null)
            {
                var player = this as Player;
                foreach (Accessory acc in player!.EquippedAccessories)
                {
                    if (acc.Stats is not null) BattleStats.CurrentEntityStats.ApplyStatChanges(acc.Stats);
                    if (acc.HiddenStats is not null) BattleStats.HiddenEntityStats.ApplyStatChanges(acc.HiddenStats);
                }
                if (player.CurrentEquippedWeapon.Stats is not null)
                {
                    BattleStats.CurrentEntityStats.ApplyStatChanges(player.CurrentEquippedWeapon.Stats);
                    BattleStats.HiddenEntityStats.ApplyStatChanges(player.CurrentEquippedWeapon.HiddenStats);
                }
            }
            foreach (var effect in BattleStats.CurrentEffects)
            {
                BattleStats.CurrentEntityStats.ApplyStatChanges(effect.Stats);
                BattleStats.HiddenEntityStats.ApplyStatChanges(effect.HiddenStats);
            }
            BattleStats.CurrentEntityStats.Health = Utils.Round(BattleStats.CurrentEntityStats.Health * BattleStats.HiddenEntityStats.HealthMultiplier);

            // Проверяем выход за рамки минимальных хп и маны
            if (Stats.Health < Constants.minimumHealth)
                Stats.Health = Constants.minimumHealth;
            if (Stats.Mana < Constants.minimumMana)
                Stats.Mana = Constants.minimumMana;

            // Проверяем, больше ли нынешнее здоровье нового максимума (допустим у нас 100 хп, а максимум теперь 75)
            // Затем, если условие выше не выполнено, проверяем изменился ли максимум, если изменился - прибавляем к нынешнему пулу
            // Специально не использую здесь методы Set/Change CurrentHealth т.к. мне не нужны здесь инвоки ивентов
            if (BattleStats.CurrentHealth > BattleStats.CurrentEntityStats.Health)
                BattleStats.CurrentHealth = BattleStats.CurrentEntityStats.Health;
            else if (BattleStats.CurrentEntityStats.Health > prevMaxHealth)
                BattleStats.CurrentHealth += BattleStats.CurrentEntityStats.Health - prevMaxHealth;
            if (BattleStats.CurrentMana > BattleStats.CurrentEntityStats.Mana)
                BattleStats.CurrentMana = BattleStats.CurrentEntityStats.Mana;
            else if (BattleStats.CurrentEntityStats.Mana > prevMaxMana)
                BattleStats.CurrentMana += BattleStats.CurrentEntityStats.Mana - prevMaxMana;
        }
    }
}
