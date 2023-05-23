using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class SandpaperAcc : GenericAccessory
    {
        internal SandpaperAcc()
        {
            Info.InternalName = "acc_sandpaper";
            Info.Name = "Наждачная бумага";

            HiddenStats.PhysDmgBonus = 5;

            SpecialDescription = String.Format("Добавляет {0} урона вашему физическому оружию", HiddenStats.PhysDmgBonus);

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Common);

            Tags.Add(Tag.Physical);
            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.Melee);
        }
    }
}
