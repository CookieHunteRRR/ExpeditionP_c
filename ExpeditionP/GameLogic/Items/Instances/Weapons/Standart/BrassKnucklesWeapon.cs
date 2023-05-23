using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class BrassKnucklesWeapon : GenericMeleeWeapon
    {
        internal BrassKnucklesWeapon()
        {
            Info.InternalName = "weapon_brassknuckles";
            Info.Name = "Кастеты";

            Attack.MinDamage = 12;
            Attack.MaxDamage = 14;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы ударяете кастетами, нанося {0} урона";

            Stats.CritChance = 10;
            Stats.Evasion = -5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.CritChance);
        }
    }
}
