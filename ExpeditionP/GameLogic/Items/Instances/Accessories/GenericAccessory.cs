using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories
{
    internal class GenericAccessory : Accessory
    {
        internal GenericAccessory() : base("acc_generic")
        {
            Info.Name = "УА ТЕСТОВЫЙ"; // Универсальный аксессуар

            SetRarity(Tag.Common);
            Info.ExpeditionTag = Tag.Other;
        }
    }
}
