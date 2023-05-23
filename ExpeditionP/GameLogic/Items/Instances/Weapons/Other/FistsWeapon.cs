using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Other
{
    internal class FistsWeapon : GenericMeleeWeapon
    {
        internal FistsWeapon()
        {
            Info.InternalName = "weapon_fists";
            Info.Name = "Кулаки";

            Attack.MinDamage = 1;
            Attack.MaxDamage = 1;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы замахиваетесь кулаками, нанося {0} урона";

            SetRarity(Tag.Common);
            Info.SetWeight(0);
            Info.SetRandomAppearance(false);
            Info.ExpeditionTag = Tag.Other;
        }
    }
}
