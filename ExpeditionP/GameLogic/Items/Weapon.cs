using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class Weapon : Item
    {
        internal Attack Attack { get; set; }
        internal EntityStats Stats { get; init; }
        internal HiddenStats HiddenStats { get; set; }

        internal Weapon(string internalName) : base(internalName)
        {
            Info.Name = Constants.defaultWeaponName;
            Stats = new EntityStats();
            HiddenStats = new HiddenStats();
        }
    }
}
