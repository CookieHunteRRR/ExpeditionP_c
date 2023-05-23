using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling
{
    internal static class EventManager
    {
        internal static WeaponEquipEvent WeaponEquipEvent = new WeaponEquipEvent();
        internal static WeaponUnequipEvent WeaponUnequipEvent = new WeaponUnequipEvent();
        internal static BattleEndEvent BattleEndEvent = new BattleEndEvent();
        internal static BattleStartEvent BattleStartEvent = new BattleStartEvent();
        internal static PlayerAttackEvent PlayerAttackEvent = new PlayerAttackEvent();
        internal static MobAttackEvent MobAttackEvent = new MobAttackEvent();
        internal static MobDeathEvent MobDeathEvent = new MobDeathEvent();
        internal static EntityHealthChangeEvent EntityHealthChangeEvent = new EntityHealthChangeEvent();
        internal static FleeAttemptEvent FleeAttemptEvent = new FleeAttemptEvent();
    }
}
