using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class GlassCannonAcc : GenericAccessory
    {
        static readonly double Power = 0.5;

        internal GlassCannonAcc()
        {
            Info.InternalName = "acc_glasscannon";
            Info.Name = "Стеклянная пушка";
            SpecialDescription = $"Увеличивает физический урон на {Power * 100}%, но снижает максимальное здоровье вдвое";

            HiddenStats.PhysDmgMultiplier = Power;
            HiddenStats.HealthMultiplier = -0.5;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Legendary);

            Tags.Add(Tag.Physical);
            Tags.Add(Tag.Offensive);
        }
    }
}
