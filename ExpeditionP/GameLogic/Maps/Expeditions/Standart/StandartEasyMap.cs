using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions.Standart
{
    internal class StandartEasyMap : BlueprintMap
    {
        internal StandartEasyMap() : base(new MapInfo("map_standart_easy", "Стандарт - легкая", null, Items.Tag.Standart))
        {
            DifficultySettings.SetDifficulty(0.8, 0.15, 1.75);

            EnemyCount = 10;
            FloorCount = 1;
            MapEnterMessage = "Вы попали в неизвестное место";
            
            MapBuilder mb = new MapBuilder();
            mb.AddStartingNode();
            mb.AddNextNode(new BlueprintNode(10));
            mb.AddNextNode(new RestNode());
            mb.AddNextNode(new EnemyNode(true));
            mb.SetEnemyNodeContent("boss_standart_headbandit");
            StartingNodes = mb.ToStartingNodesList();
        }
    }
}
