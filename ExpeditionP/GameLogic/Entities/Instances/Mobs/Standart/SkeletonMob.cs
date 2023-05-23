using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class SkeletonMob : Mob
    {
        internal SkeletonMob() : base("Скелет", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_MobGeneric(5, 10, DamageType.Physical, "Скелет бьет вас, нанося {0} урона"));
            AI.AddAbilityToPool(new Ability_Skeleton_BoneEvasiveness());

            EntityTags.Add(Items.Tag.Bloodless);
            EntityTags.Add(Items.Tag.Fleshless);
        }

        class Ability_Skeleton_BoneEvasiveness : Ability
        {
            internal Ability_Skeleton_BoneEvasiveness() : base(8, 0) { }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);

                manager.SendToLog("Вы осознаете, что сражаетесь со скелетом. Противник стал уворотливее.");
                caster.BattleStats.ApplyEffect(new Effect_Buff_EvasionBoost(20));
            }
        }
    }
}
