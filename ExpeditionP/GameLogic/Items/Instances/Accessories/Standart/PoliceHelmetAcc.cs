using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class PoliceHelmetAcc : GenericAccessory
    {
        internal PoliceHelmetAcc()
        {
            Info.InternalName = "acc_policehelmet";
            Info.Name = "Полицейский шлем";

            Stats.Health = 15;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Health);
        }
    }
}
