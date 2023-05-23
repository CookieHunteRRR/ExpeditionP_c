using ExpeditionP.GameLogic.BattleLogic.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    internal class BattleStats
    {
        Entity Entity { get; init; } // чьи статы
        public EntityStats CurrentEntityStats { get; set; }
        public HiddenStats HiddenEntityStats { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentMana { get; set; }
        public List<Effect> CurrentEffects { get; set; }
        public List<string> Immunities { get; }

        public BattleStats(Entity entity, EntityStats originalStats)
        {
            Entity = entity;
            CurrentEntityStats = originalStats.Copy();
            HiddenEntityStats = new HiddenStats();
            CurrentHealth = CurrentEntityStats.Health;
            CurrentMana = CurrentEntityStats.Mana;
            CurrentEffects = new List<Effect>();
        }

        public void ApplyEffect(Effect effect)
        {
            // Проверка на иммунитет к эффекту
            if (effect.IsEffectIgnored(Entity))
            {
                Program.Expedition.SendToLog($"Не удалось наложить эффект {effect.Name} на {Entity.GetName()} (ИММУНИТЕТ)");
                return;
            }

            var stackingType = effect.StackingType;
            Effect? oldEffect = FindEffectOfType(effect.GetType());
            // Проверяем, наложен ли вообще такой эффект
            if (oldEffect is null)
            {
                CurrentEffects.Add(effect);
                Entity.RecalculateStats();
                return;
            }

            switch (stackingType)
            {
                case EffectStackingType.None:
                    // Проверяем, сильнее ли новый эффект уже наложенного
                    if (oldEffect.Power <= effect.Power)
                    {
                        // Если сильнее, то заменяем
                        CurrentEffects.Remove(oldEffect);
                        break;
                    }
                    // Если же новый эффект слабее старого - ничего не происходит
                    return;
                case EffectStackingType.Stackable:
                    ApplyEffectStack(oldEffect);
                    return;
                case EffectStackingType.Dupable:
                    break;
            }
            CurrentEffects.Add(effect);
            Entity.RecalculateStats();
        }

        public void UndoEffect(Effect effect)
        {
            CurrentEffects.Remove(effect);
            Entity.RecalculateStats();
        }

        void ApplyEffectStack(Effect toApply)
        {
            var stackableEffect = toApply as StackableEffect;
            if (stackableEffect == null)
            {
                throw new Exception("Я забыл сделать стакаемый эффект стакаемым эффектом");
            }
            // Проверяем, не замакшен ли эффект
            if (!stackableEffect.IsReachedMaxStacks())
            {
                toApply.Power += 1;
                stackableEffect.UpdateEffectPower();
            }
            // Замакшен или нет - обновляем время действия в любом случае
            toApply.TurnsLeft = toApply.Duration;
            Entity.RecalculateStats();
        }

        /// <summary>
        /// Возвращает эффект, соответствующий входному типу. Если таких эффектов несколько, возвращает первый встречный. Если таких эффектов нет - возвращает null.
        /// </summary>
        /// <param name="effectType"></param>
        /// <returns></returns>
        internal Effect? FindEffectOfType(Type effectType)
        {
            foreach (Effect eff in CurrentEffects)
            {
                if (eff.GetType() == effectType) return eff;
            }
            return null;
        }
    }
}
