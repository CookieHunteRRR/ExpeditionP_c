using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Other
{
    internal class TestPleaseIgnoreWeapon : Weapon
    {
        internal TestPleaseIgnoreWeapon() : base("weapon_tpi")
        {
            Info.Name = "Нож-бабочка";
            Attack = new Attack_PlayerGeneric(20, 100, DamageType.Physical, "тест");
            Info.Description = "тест";

            //Stats.CritDamage = 10;

            SetRarity(Tag.Common);
            Info.ExpeditionTag = Tag.Other;
            Info.SetWeight(0);
            Info.SetRandomAppearance(false);

            //Tags.Add(Tag.Physical);
        }
    }
}
