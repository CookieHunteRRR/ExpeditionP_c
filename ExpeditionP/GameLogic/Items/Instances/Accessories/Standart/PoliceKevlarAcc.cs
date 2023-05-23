using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class PoliceKevlarAcc : GenericAccessory
    {
        internal PoliceKevlarAcc()
        {
            Info.InternalName = "acc_policekevlar";
            Info.Name = "Полицейский бронежилет";

            Stats.Health = 25;
            Stats.Defense = 5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Health);
            Tags.Add(Tag.Defense);
        }
    }
}
