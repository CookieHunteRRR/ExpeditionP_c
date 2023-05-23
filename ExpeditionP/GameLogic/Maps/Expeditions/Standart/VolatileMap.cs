using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions.Standart
{
    internal class VolatileMap : BlueprintMap
    {
        internal VolatileMap() : base(new MapInfo("map_standart_volatile", "Изменчивое место", null, Items.Tag.Standart))
        {
            DifficultySettings.SetDifficulty(0.6, 0.15, 1.5);

            EnemyCount = 10;
            FloorCount = 1;
            MapEnterMessage = "Вы попали в неизвестное место";

            MapBuilder mb = new MapBuilder();
            mb.AddStartingNode();
            mb.AddNextNode(new BlueprintNode(10));
            StartingNodes = mb.ToStartingNodesList();
        }
    }
}
