using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    /// <summary>
    /// Класс, который содержит достаточную информацию для сохранения прогресса по игре
    /// Будь то полученная игроком экипировка с экспедиций, пройденные экспедиции и статы самого игрока
    /// </summary>
    internal class GameInstance
    {
        internal Player Player { get; init; }
        internal List<string> CollectedItems { get; init; }

        internal GameInstance()
        {
            Player = new Player();
            CollectedItems = new List<string>();
        }

        internal void AddItemToCollectedItems(string itemId)
        {
            CollectedItems.Add(itemId);
        }

        internal GameInstance GetInstanceForExpedition()
        {
            Player.SetupToExpedition();
            return this;
        }
    }
}
