using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling
{
    internal enum EventType
    {
        BattleStart,
        BattleEnd,
        PlayerEvaded,
        EnemyEvaded,
        WeaponEquipped,
        WeaponUnequipped
    }
}
