using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories
{
    internal class UsableAccessory : Accessory
    {
        internal Ability? Ability { get; set; }

        internal UsableAccessory() : base("acc_genericusable")
        {
            Info.Name = "USABLE ТЕСТОВЫЙ"; // Универсальный аксессуар

            SetRarity(Tag.Common);
            Info.ExpeditionTag = Tag.Other;

            Ability = null;
        }

        internal void UseAbility(ExpeditionManager manager)
        {
            Ability.Activate(manager, manager.GameInstance.Player);
        }
    }
}
