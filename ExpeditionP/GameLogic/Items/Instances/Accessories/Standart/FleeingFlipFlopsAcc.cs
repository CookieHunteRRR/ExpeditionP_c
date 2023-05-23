using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class FleeingFlipFlopsAcc : GenericAccessory
    {
        internal FleeingFlipFlopsAcc()
        {
            Info.InternalName = "acc_fleeingflipflops";
            Info.Name = "Сбегательные шлепки";

            Stats.Evasion = 5;
            HiddenStats.FleeChance = 10;
            SpecialDescription = $"Увеличивает шанс побега на {HiddenStats.FleeChance}%";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Evasion);
        }
    }
}
