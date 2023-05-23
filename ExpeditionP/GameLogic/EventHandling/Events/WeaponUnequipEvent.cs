using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class WeaponUnequipEvent : Event<EventArgs>
    {
        internal delegate void WeaponUnequipDlg(ExpeditionManager? manager);
        internal event WeaponUnequipDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, EventArgs? args = null)
        {
            UserEvent?.Invoke(manager);
        }
    }
}