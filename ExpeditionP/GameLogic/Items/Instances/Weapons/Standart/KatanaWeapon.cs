using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class KatanaWeapon : GenericMeleeWeapon
    {
        internal KatanaWeapon()
        {
            Info.InternalName = "weapon_katana";
            Info.Name = "Катана";

            Attack.MinDamage = 20;
            Attack.MaxDamage = 25;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы замахиваетесь катаной, нанося {0} урона";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
        }
    }
}
