using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling.Events
{
    internal class EntityHealthChangeEvent : Event<EntityHealthChangeEventArgs>
    {
        internal delegate void EntityHealthChangeDlg(ExpeditionManager? manager, EntityHealthChangeEventArgs? args);
        internal event EntityHealthChangeDlg UserEvent;

        internal override void Invoke(ExpeditionManager? manager, EntityHealthChangeEventArgs? args)
        {
            UserEvent?.Invoke(manager, args);
        }
    }

    internal class EntityHealthChangeEventArgs : EventArgs
    {
        internal bool IsEntityAPlayer { get; set; }
        internal bool IsHealthLowered { get; set; } // true - получен урон, false - восстановлено здоровье

        internal EntityHealthChangeEventArgs(bool isPlayer, bool isDamaged)
        {
            IsEntityAPlayer = isPlayer;
            IsHealthLowered = isDamaged;
        }
    }
}
