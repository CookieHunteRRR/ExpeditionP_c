using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class WerewolfMob : Mob
    {
        internal WerewolfMob() : base("Оборотень", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_MobGeneric(5, 8, DamageType.Physical, "Оборотень атакует вас, нанося {0} урона"));
            AI.AddAbilityToPool(new Ability_Werewolf_Transformation());

            Stats.Health = 75;

            Info.SetDifficultyMultiplierRequirements(1, 100);
        }

        class Ability_Werewolf_Transformation : Ability
        {
            internal Ability_Werewolf_Transformation() : base(6, 0) { }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);

                manager.SendToLog("Оборотень превратился в волка.");
                caster.BattleStats.ApplyEffect(new Effect_Buff_Transformation());
            }
        }

        class Effect_Buff_Transformation : Effect
        {
            internal Effect_Buff_Transformation() : base(EffectType.Buff, 0)
            {
                Name = "Превращение";
                Description = String.Format("Обладатель эффекта превращен в волка. Его характеристики увеличены.");
                ActivationType = Items.ActivationType.None;

                Stats.Health = 75;
                Stats.Defense = 10;
                Stats.Evasion = 10;
                HiddenStats.PhysDmgBonus = 5;

                SetInitialDuration(5);
            }
        }
    }
}
