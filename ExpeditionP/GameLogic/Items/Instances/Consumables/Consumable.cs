using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Consumables
{
    internal class Consumable : Item
    {
        internal int Power { get; set; }

        internal Consumable(string internalName) : base(internalName)
        {
            Info.Name = internalName;
            Info.ExpeditionTag = Tag.Other;
        }

        internal virtual void Consume(ExpeditionManager manager) { }
    }
}
