using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Consumables.Other
{
    internal class AppleConsumable : Consumable
    {
        internal AppleConsumable() : base("consumable_apple")
        {
            Power = 2;

            Info.Name = "Яблоко";
            Info.Description = $"Восстанавливает {Power} здоровья";
            SetRarity(Tag.Common);
        }

        internal override void Consume(ExpeditionManager manager)
        {
            Player player = manager.GameInstance.Player;

            player.ChangeCurrentHealth(Power, manager);
            manager.SendToLog($"Вы съели {Info.Name}, восстановив {Power} здоровья");
        }
    }
}
