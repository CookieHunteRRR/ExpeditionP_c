using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Consumables.Other
{
    internal class BandageConsumable : Consumable
    {
        internal BandageConsumable() : base("consumable_bandage")
        {
            Power = 5;

            Info.Name = "Бинты";
            Info.Description = $"Восстанавливает {Power} здоровья";
            SetRarity(Tag.Uncommon);
        }

        internal override void Consume(ExpeditionManager manager)
        {
            Player player = manager.GameInstance.Player;

            player.ChangeCurrentHealth(Power, manager);
            manager.SendToLog($"Вы перебинтовались, восстановив {Power} здоровья");
        }
    }
}
