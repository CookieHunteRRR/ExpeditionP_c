using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.BattleLogic.Effects
{
    /// <summary>
    /// Изменяет статы Entity на определенное время, либо на весь бой
    /// </summary>
    internal class Effect
    {
        internal bool IsHidden { get; set; }
        internal bool IsRemovedOnBattleEnd { get; set; }
        internal string Name { get; set; }
        internal string Description { get; set; }
        internal EffectType EffectType { get; set; }
        internal ActivationType ActivationType { get; set; }
        internal EffectStackingType StackingType { get; set; }
        internal List<Tag> IgnoredBy { get; init; } // список тегов энтити, которые будут игнорировать этот эффект
        internal int Power { get; set; }
        internal int Duration { get; private set; } // Изначальное количество ходов, которое будет действовать данный эффект
        internal EntityStats Stats { get; set; } // Изменения статов, которые необходимо применить к EntityStats
        internal HiddenStats HiddenStats { get; set; }
        internal int TurnsLeft { get; set; } // Оставшаяся длительность эффекта

        internal Effect(EffectType type, int power = 0)
        {
            EffectType = type;
            Power = power;

            IsHidden = false;
            IsRemovedOnBattleEnd = true;
            StackingType = EffectStackingType.None;
            IgnoredBy = new List<Tag>();
            Stats = new EntityStats();
            HiddenStats = new HiddenStats();
        }

        internal bool IsEffectIgnored(Entity target)
        {
            foreach (var tag in IgnoredBy)
                if (target.EntityTags.Contains(tag)) return true;
            return false;
        }

        internal void SetInitialDuration(int duration) { Duration = duration; TurnsLeft = Duration; }

        internal virtual void ApplySpecialEffect(ExpeditionManager manager, Entity effectOwner) { }
    }
}
