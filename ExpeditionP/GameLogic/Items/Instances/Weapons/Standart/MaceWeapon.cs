using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class MaceWeapon : GenericMeleeWeapon
    {
        internal MaceWeapon()
        {
            Info.InternalName = "weapon_mace";
            Info.Name = "Булава";

            Attack.MinDamage = 15;
            Attack.MaxDamage = 20;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы ударяете булавой, нанося {0} урона";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
        }
    }
}
