using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class PoliceArmorSetAcc : GenericAccessory
    {
        internal PoliceArmorSetAcc()
        {
            Info.InternalName = "acc_policearmorset";
            Info.Name = "Полицейский бронекомплект";

            Stats.Health = 30;
            Stats.Defense = 10;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Health);
            Tags.Add(Tag.Defense);
        }
    }
}
