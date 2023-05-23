using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class FleeAttemptEvent : Event<FleeAttemptEventArgs>
    {
        internal delegate void FleeAttemptDlg(ExpeditionManager? manager, FleeAttemptEventArgs? args);
        internal event FleeAttemptDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, FleeAttemptEventArgs? args)
        {
            UserEvent?.Invoke(manager, args);
        }
    }

    internal class FleeAttemptEventArgs : EventArgs
    {
        internal bool IsCancelled { get; set; }

        internal FleeAttemptEventArgs()
        {
            this.IsCancelled = false;
        }
    }
}
