using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class LifestealerWeapon : Weapon
    {
        static readonly string attackMessage = "Вы разрываете врага, нанося {0} урона";

        static readonly int healOnSuccessfulAttack = 3;

        internal LifestealerWeapon() : base("weapon_lifestealer")
        {
            Info.Name = "Похититель жизни";
            Info.Description = "Кинжал, напоминающий иглу, передающий жизненную силу врага";
            Attack = new Attack_Lifestealer();

            HiddenStats.Vampirism = 10;
            SpecialDescription = $"Увеличивает вампиризм на {HiddenStats.Vampirism}%\n" +
                $"В случае успешной атаки восстанавливает {healOnSuccessfulAttack} здоровья";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.Vampiric);
        }

        class Attack_Lifestealer : Attack
        {
            internal Attack_Lifestealer()
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
                    manager.GameInstance.Player.ChangeCurrentHealth(healOnSuccessfulAttack);
                }
            }
        }
    }
}
