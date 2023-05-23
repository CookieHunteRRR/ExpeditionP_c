using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class WhetstoneAcc : GenericAccessory
    {
        internal WhetstoneAcc()
        {
            Info.InternalName = "acc_whetstone";
            Info.Name = "Точильный камень";

            Stats.CritChance = 10;
            Stats.CritDamage = 10;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);

            Tags.Add(Tag.Critical);
            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.CritDamage);
            Tags.Add(Tag.Melee);
        }
    }
}
