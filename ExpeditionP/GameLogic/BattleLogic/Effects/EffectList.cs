using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;

namespace ExpeditionP.GameLogic.BattleLogic.Effects
{
    internal class EffectList
    {
        internal class Effect_Debuff_DefenseCut : Effect
        {
            internal Effect_Debuff_DefenseCut(int power) : base(EffectType.Debuff, power)
            {
                Name = "Срез брони";
                Description = string.Format("Понижает значение брони на {0}%", Power);
                ActivationType = ActivationType.None;
                Stats.Defense = -Power;
                SetInitialDuration(3);
            }
        }

        internal class Effect_Debuff_Poison : Effect
        {
            internal Effect_Debuff_Poison(int power) : base(EffectType.Debuff, power)
            {
                Name = "Отравление";
                Description = string.Format("Наносит {0} урона обладателю эффекта в конце каждого хода", Power);
                ActivationType = ActivationType.PostTurn;
                StackingType = EffectStackingType.Dupable;
                SetInitialDuration(3);
            }

            internal override void ApplySpecialEffect(ExpeditionManager manager, Entity effectOwner)
            {
                effectOwner.ChangeCurrentHealth(-Power, manager);
                manager.SendToLog(effectOwner.GetName() + " получает " + Power + " урона от отравления");
                manager.BattleManager.CheckHealth();
            }
        }

        internal class Effect_Buff_EvasionBoost : Effect
        {
            internal Effect_Buff_EvasionBoost(int power) : base(EffectType.Buff, power)
            {
                Name = "Уворотливость";
                Description = string.Format("Увеличивает уворотливость обладателя эффекта на {0}", Power);
                ActivationType = ActivationType.None;
                Stats.Evasion += Power;
                SetInitialDuration(3);
            }
        }

        internal class Effect_Debuff_DeepWound : StackableEffect
        {
            internal Effect_Debuff_DeepWound() : base(EffectType.Debuff, 5)
            {
                Name = "Глубокая рана";
                Description = "Жертва получает увеличенный физический урон";
                SetInitialDuration(6);

                IgnoredBy.Add(Tag.Fleshless);

                ActivationType = ActivationType.None;
            }
        }

        internal class Effect_Buff_Impatience : StackableEffect
        {
            internal Effect_Buff_Impatience() : base(EffectType.Buff, 10)
            {
                Name = "Нетерпеливость";
                Description = "Обладатель эффекта получает увеличенный шанс критического попадания и увеличенный физический урон";
                SetInitialDuration(5);

                ActivationType = ActivationType.None;

                UpdateEffectPower();
            }

            internal override void UpdateEffectPower()
            {
                Stats.CritChance = Power * 5;
                HiddenStats.PhysDmgBonus = Power * 2;
            }
        }

        internal class Effect_Buff_Bloodlust : Effect
        {
            internal Effect_Buff_Bloodlust(int power) : base(EffectType.Buff, power)
            {
                Name = "Жажда крови";
                Description = string.Format("Увеличивает вампиризм на {0}%", Power);
                ActivationType = ActivationType.None;
                HiddenStats.Vampirism += Power;
                SetInitialDuration(1);
            }
        }
    }
}
