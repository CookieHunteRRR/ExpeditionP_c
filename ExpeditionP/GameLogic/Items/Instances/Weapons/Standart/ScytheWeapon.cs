using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class ScytheWeapon : GenericMeleeWeapon
    {
        internal ScytheWeapon()
        {
            Info.InternalName = "weapon_scythe";
            Info.Name = "Коса";

            Attack.MinDamage = 18;
            Attack.MaxDamage = 22;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы косите противника, нанося {0} урона";

            Stats.CritChance = 5;
            Stats.CritDamage = 25;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.CritDamage);
            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.Critical);
        }
    }
}
