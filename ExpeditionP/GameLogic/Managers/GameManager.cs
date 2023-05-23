using ExpeditionP.GameLogic.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Managers
{
    internal class GameManager
    {
        internal MapManager MapManager { get; init; }

        public GameManager()
        {
            MapManager = new MapManager();
        }
    }
}
