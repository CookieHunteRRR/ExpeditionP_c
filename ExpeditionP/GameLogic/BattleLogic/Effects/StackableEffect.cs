using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.BattleLogic.Effects
{
    internal class StackableEffect : Effect
    {
        internal int MaxStackCount { get; init; }

        internal StackableEffect(EffectType type, int maxStackCount, int power = 1) : base(type, power)
        {
            MaxStackCount = maxStackCount;
            base.StackingType = EffectStackingType.Stackable;
        }

        internal bool IsReachedMaxStacks() { return Power >= MaxStackCount; }
        internal virtual void UpdateEffectPower() { }
    }
}
