using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps
{
    internal class Map
    {
        internal MapInfo Info { get; init; }
        internal List<MapNode> StartingNodes { get; init; }
        internal LootTable LootTable { get; init; }
        internal DifficultySettings DifficultySettings { get; init; }

        internal Map(MapInfo info, LootTable lootTable)
        {
            Info = info;

            if (!Constants.expeditionTags.Contains((Tag)Info.ExpeditionTag))
            {
                string wrongTag = Info.ExpeditionTag.ToString();
                Info.ExpeditionTag = Tag.Other;
                Program.SendToLog(String.Format("[Map] При регистрации карты {0} был использован несуществующий тег экспедиции {1}," +
                    " установлен тег Other", Info.InternalName, wrongTag));
            }

            StartingNodes = new List<MapNode>();
            LootTable = lootTable;
            DifficultySettings = new DifficultySettings();
        }

        internal void AddStartingNode(MapNode node) { StartingNodes.Add(node); }
    }
}
