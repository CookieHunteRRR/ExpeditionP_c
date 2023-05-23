using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class PlayerAttackEvent : Event<PlayerAttackEventArgs>
    {
        internal delegate void PlayerAttackDlg(ExpeditionManager? manager, PlayerAttackEventArgs? args);
        internal event PlayerAttackDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, PlayerAttackEventArgs? args)
        {
            UserEvent?.Invoke(manager, args);
        }
    }

    internal class PlayerAttackEventArgs : EventArgs
    {
        internal bool IsNotEvaded { get; set; }
        internal bool IsCrit { get; set; }

        internal PlayerAttackEventArgs(bool isNotEvaded, bool isCrit)
        {
            this.IsNotEvaded = isNotEvaded;
            this.IsCrit = isCrit;
        }
    }
}
