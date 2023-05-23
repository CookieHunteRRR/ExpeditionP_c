using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class FleshripperWeapon : Weapon
    {
        static readonly string attackMessage = "Вы разрываете врага, нанося {0} урона";

        internal FleshripperWeapon() : base("weapon_fleshripper")
        {
            Info.Name = "Потрошитель плоти";
            Attack = new Attack_Fleshripper();
            SpecialDescription = "Каждая успешная атака накладывает на врага стакающийся эффект" +
                " \"Глубокая рана\", увеличивающий получаемый врагом физический урон";

            Stats.CritChance = -100;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Legendary);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
        }

        class Attack_Fleshripper : Attack
        {
            internal Attack_Fleshripper()
            {
                MinDamage = 15;
                MaxDamage = 20;
                DamageType = DamageType.Physical;
                Message = attackMessage;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                var attInst = battle.MakeAttack(this, true);
                if (attInst.Damage > 0)
                {
                    var effect = new Effect_Debuff_DeepWound();
                    battle.Enemy.BattleStats.ApplyEffect(effect);
                }
            }
        }
    }
}
