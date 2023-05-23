using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class BattleEndEvent : Event<BattleEndEventArgs>
    {
        internal delegate void BattleEndDlg(ExpeditionManager? manager, BattleEndEventArgs? args);
        internal event BattleEndDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, BattleEndEventArgs? args = null)
        {
            UserEvent?.Invoke(manager, args);
        }
    }

    internal class BattleEndEventArgs : EventArgs
    {
        internal BattleFinishReason BattleFinishReason { get; set; }

        internal BattleEndEventArgs(BattleFinishReason reason)
        {
            this.BattleFinishReason = reason;
        }
    }
}
