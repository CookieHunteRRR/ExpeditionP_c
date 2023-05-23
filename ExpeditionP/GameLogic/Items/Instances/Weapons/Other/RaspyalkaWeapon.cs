using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Other
{
    internal class RaspyalkaWeapon : GenericMeleeWeapon
    {
        internal RaspyalkaWeapon()
        {
            Info.InternalName = "weapon_raspyalka";
            Info.Name = "Распялка";
            Info.Description = "Палка с заостренным концом. Поговаривают, что таким инструментом прибивали к кресту приговоренных к смертной казни. Любимое оружие ненавистников паркета.";

            Attack.MinDamage = 15;
            Attack.MaxDamage = 20;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы протыкаете врага, нанося {0} урона";

            SetRarity(Tag.Uncommon);
            Info.SetRandomAppearance(false);
            Info.ExpeditionTag = Tag.Other;

            Stats.CritChance = 15;
        }
    }
}
