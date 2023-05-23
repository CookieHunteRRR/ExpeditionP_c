using ExpeditionP.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class Accessory : Item
    {
        internal EntityStats Stats { get; init; }
        internal HiddenStats HiddenStats { get; init; }

        internal Accessory(string internalName) : base(internalName)
        {
            Info.Name = Constants.defaultWeaponName;
            Stats = new EntityStats();
            HiddenStats = new HiddenStats();
        }
    }
}
