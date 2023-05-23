using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.EventHandling
{
    internal class EventSubject
    {
        internal bool IsReactingToEvents { get; set; } = false;

        internal virtual EventSubject Copy()
        {
            EventSubject eventSubject = new EventSubject();
            CopyData(eventSubject);
            return eventSubject;
        }

        protected virtual void CopyData(object copy)
        {
            EventSubject correctedCopy = (EventSubject)copy;
            correctedCopy.IsReactingToEvents = this.IsReactingToEvents;
        }

        internal virtual void RegisterEvents() { }
        internal virtual void UnregisterEvents() 
        { if (IsReactingToEvents) throw new Exception("[EventSubject] Реагирующий на ивент класс " + this.GetType().FullName + " не реализует отмену регистрации ивентов"); }
    }
}