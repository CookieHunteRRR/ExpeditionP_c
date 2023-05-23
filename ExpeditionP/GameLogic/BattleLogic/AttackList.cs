using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.BattleLogic
{
    internal class AttackList
    {
        internal class Attack_PlayerGeneric : Attack
        {
            internal Attack_PlayerGeneric(double mindmg, double maxdmg, DamageType type, string message)
            {
                MinDamage = mindmg;
                MaxDamage = maxdmg;
                DamageType = type;
                Message = message;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                battle.MakeAttack(this, true);
            }
        }

        internal class Attack_MobGeneric : Attack
        {
            internal Attack_MobGeneric(double mindmg, double maxdmg, DamageType type, string message)
            {
                MinDamage = mindmg;
                MaxDamage = maxdmg;
                DamageType = type;
                Message = message;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                battle.MakeAttack(this, false);
            }
        }

        // В общем листе так как используется 2 мобами
        internal class Attack_Bandit : Attack
        {
            static readonly double doubleAttackChance = 0.3;
            static readonly string attackMessage = "Бандит бьет вас, нанося {0} урона";
            static readonly string doubleAttackMessage = "Бандит бьет вас вновь, нанося {0} урона";

            internal Attack_Bandit()
            {
                MinDamage = 8;
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
    }
}
