using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class BanditMob : Mob
    {
        internal BanditMob() : base("Бандит", Tag.Standart)
        {
            AI.AddAttackToPool(new AttackList.Attack_Bandit());

            Stats.Health = 80;
        }
    }
}
