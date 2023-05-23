using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class WeaponEquipEvent : Event<EventArgs>
    {
        internal delegate void WeaponEquipDlg(ExpeditionManager? manager);
        internal event WeaponEquipDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, EventArgs? args = null)
        {
            UserEvent?.Invoke(manager);
        }
    }
}
