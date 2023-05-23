using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class MobDeathEvent : Event<EventArgs>
    {
        internal bool IsCancelled { get; set; }

        internal delegate void MobDeathDlg(ExpeditionManager? manager);
        internal event MobDeathDlg UserEvent;

        internal MobDeathEvent() { IsCancelled = false; }

        internal override void Invoke(ExpeditionManager? manager, EventArgs? args = null)
        {
            UserEvent?.Invoke(manager);
        }
    }
}
