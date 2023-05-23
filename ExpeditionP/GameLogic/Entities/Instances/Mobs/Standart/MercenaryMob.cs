using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class MercenaryMob : Mob
    {
        internal MercenaryMob() : base("Наемник", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_MobGeneric(10, 12, DamageType.Physical, "Наемник атакует вас, нанося {0} урона"));

            Stats.Defense = 5;
        }
    }
}
