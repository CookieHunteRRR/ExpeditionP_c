using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class AxeWeapon : GenericMeleeWeapon
    {
        internal AxeWeapon()
        {
            Info.InternalName = "weapon_axe";
            Info.Name = "Топор";

            Attack.MinDamage = 12;
            Attack.MaxDamage = 16;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы рубите противника, нанося {0} урона";

            Stats.CritDamage = 10;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.CritDamage);
        }
    }
}
