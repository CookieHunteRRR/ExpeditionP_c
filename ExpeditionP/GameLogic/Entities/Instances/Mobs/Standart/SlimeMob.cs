using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class SlimeMob : Mob
    {
        static readonly string attackMessage = "Слизень неприятно касается вас, отравляя и нанося {0} урона";
        static readonly double debuffChance = 0.2;

        internal SlimeMob() : base("Слизень", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_Slime());

            EntityTags.Add(Items.Tag.Bloodless);
            EntityTags.Add(Items.Tag.Fleshless);

            Stats.Health = 50;
            Stats.Evasion = 20;

            Info.SetDifficultyMultiplierRequirements(1, 100);
        }

        class Attack_Slime : Attack
        {
            internal Attack_Slime()
            {
                MinDamage = 4;
                MaxDamage = 6;
                DamageType = DamageType.Physical;
                Message = attackMessage;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                battle.MakeAttack(this, false);
                battle.Player.BattleStats.ApplyEffect(new EffectList.Effect_Debuff_Poison(10));
                if (Utils.CheckProbability(debuffChance))
                {
                    manager.SendToLog("Токсичное касание слизня оставляет на вас мерзкую слизь, замедляющую вас и разъедающую вашу броню");
                    battle.Player.BattleStats.ApplyEffect(new Effect_Debuff_ToxicTouch());
                }
            }
        }

        class Effect_Debuff_ToxicTouch : StackableEffect
        {
            internal Effect_Debuff_ToxicTouch() : base(EffectType.Debuff, 3)
            {
                Name = "Токсичное прикосновение";
                Description = String.Format("Броня жертвы разъедается, а сама жертва становится менее уворотливой");
                SetInitialDuration(4);

                ActivationType = Items.ActivationType.None;
                StackingType = EffectStackingType.Stackable;

                UpdateEffectPower();
            }

            internal override void UpdateEffectPower()
            {
                Stats.Defense = -5 * Power;
                Stats.Evasion = -10 * Power;
            }
        }
    }
}
