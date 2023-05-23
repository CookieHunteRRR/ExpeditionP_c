using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class MobAttackEvent : Event<MobAttackEventArgs>
    {
        internal delegate void MobAttackDlg(ExpeditionManager? manager, MobAttackEventArgs? args);
        internal event MobAttackDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, MobAttackEventArgs? args)
        {
            UserEvent?.Invoke(manager, args);
        }
    }

    internal class MobAttackEventArgs : EventArgs
    {
        internal bool IsNotEvaded { get; set; }
        internal bool IsCrit { get; set; }

        internal MobAttackEventArgs(bool isNotEvaded, bool isCrit)
        {
            this.IsNotEvaded = isNotEvaded;
            this.IsCrit = isCrit;
        }
    }
}
