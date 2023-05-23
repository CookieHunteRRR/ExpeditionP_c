using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class ButterflyKnifeWeapon : Weapon
    {
        static readonly double backstabChance = 0.2;
        static readonly string defaultMessage = "Вы атакуете противника ножом-бабочкой, нанося {0} урона";
        static readonly string backstabMessage = "Вы ударяете противника в спину, нанося {0} урона";

        internal ButterflyKnifeWeapon() : base("weapon_butterflyknife")
        {
            Info.Name = "Нож-бабочка";
            Attack = new Attack_ButterflyKnife();
            SpecialDescription = String.Format("Атакуя этим оружием вы имеете {0}% шанс ударить врага в спину", 
                backstabChance * 100);

            Stats.CritChance = 10;
            Stats.CritDamage = 10;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.Critical);
        }

        class Attack_ButterflyKnife : Attack
        {
            internal Attack_ButterflyKnife()
            {
                MinDamage = 16;
                MaxDamage = 18;
                DamageType = DamageType.Physical;
                Message = defaultMessage;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                if (Utils.CheckProbability(backstabChance))
                {
                    battle.Player.BattleStats.ApplyEffect(new Effect_Hidden_Backstab());
                    Message = backstabMessage;
                    battle.MakeAttack(this, true);
                    Message = defaultMessage;
                    return;
                }
                battle.MakeAttack(this, true);
            }
        }

        class Effect_Hidden_Backstab : Effect
        {
            internal Effect_Hidden_Backstab() : base(EffectType.Buff, 0)
            {
                IsHidden = true;
                Name = "backstab_effect";
                Description = "backstab";
                SetInitialDuration(1);
                ActivationType = ActivationType.None;

                HiddenStats.PhysDmgBonus = 5;
                HiddenStats.GuaranteedCrit = true;
            }
        }
    }

    
}
