using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class WoodenShieldAcc : GenericAccessory
    {
        internal WoodenShieldAcc()
        {
            Info.InternalName = "acc_woodenshield";
            Info.Name = "Деревянный щит";

            Stats.Defense = 5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Defense);
        }
    }
}
