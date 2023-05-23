using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling
{
    internal class Event<T> where T : EventArgs
    {
        //internal delegate void UI(ExpeditionManager manager);
        internal virtual void Invoke(ExpeditionManager? manager, T? eventArgs) { }
    }
}
