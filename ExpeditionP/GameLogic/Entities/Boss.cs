using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    internal class Boss : Mob
    {
        internal Dictionary<int, Ability> Triggers { get; set; }
        internal bool[] IsTriggerActivated { get; set; }

        internal Boss(string name, Tag expeditionTag) : base(name, expeditionTag)
        {
            Info.InternalName = GetMobInternalName();

            Triggers = new Dictionary<int, Ability>();
            IsTriggerActivated = new bool[Triggers.Count];
        }

        internal void AddTrigger(int hpPercent, Ability trigger)
        {
            Triggers.Add(hpPercent, trigger);
            IsTriggerActivated = new bool[Triggers.Count];
        }

        internal override Boss Copy()
        {
            Boss copy = new Boss(GetName(), (Tag)Info.ExpeditionTag);
            CopyData(copy);
            return copy;
        }

        protected override void CopyData(object copy)
        {
            Boss correctedCopy = (Boss)copy;
            base.CopyData(correctedCopy);
            correctedCopy.Triggers = new Dictionary<int, Ability>(Triggers);
            correctedCopy.IsTriggerActivated = new bool[Triggers.Count];
        }
    }
}
