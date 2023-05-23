using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class BrokenSwordWeapon : GenericMeleeWeapon
    {
        internal BrokenSwordWeapon()
        {
            Info.InternalName = "weapon_brokensword";
            Info.Name = "Сломанный меч";

            Attack.MinDamage = 8;
            Attack.MaxDamage = 10;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы замахиваетесь мечом, нанося {0} урона";

            Stats.CritChance = 5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.CritChance);
        }
    }
}
