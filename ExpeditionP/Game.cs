using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    internal class Game
    {
        internal GameInstance? GameInstance { get; set; }
        internal GameManager GameManager { get; init; }

        internal Game()
        {
            GameManager = new GameManager();
        }

        internal void LoadGameContent()
        {
            ItemHolder.RegisterItems();
            EntityHolder.RegisterEntities();
            GameManager.MapManager.LoadAllMaps();
        }

        public void CreateGameInstance()
        {
            GameInstance = new GameInstance();
            Program.Hideout.LoadHideout();
        }
    }
}
