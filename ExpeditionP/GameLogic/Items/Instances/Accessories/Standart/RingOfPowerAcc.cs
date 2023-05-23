using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class RingOfPowerAcc : GenericAccessory
    {
        internal RingOfPowerAcc()
        {
            Info.InternalName = "acc_ringofpower";
            Info.Name = "Кольцо мощи";

            HiddenStats.PhysDmgBonus = 10;
            SpecialDescription = String.Format("Добавляет {0} урона вашему физическому оружию", HiddenStats.PhysDmgBonus);
            Stats.CritChance = -20;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);

            Tags.Add(Tag.Physical);
        }
    }
}
