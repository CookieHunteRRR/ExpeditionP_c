using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class AnatomyBookAcc : GenericAccessory
    {
        internal AnatomyBookAcc()
        {
            Info.InternalName = "acc_anatomybook";
            Info.Name = "Учебник по анатомии";

            Stats.CritChance = 5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Critical);
            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.CritChance);
        }
    }
}
