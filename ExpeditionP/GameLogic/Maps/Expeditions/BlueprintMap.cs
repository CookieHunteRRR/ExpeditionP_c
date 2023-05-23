using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions
{
    internal class BlueprintMap : Map
    {
        internal int EnemyCount { get; set; }
        internal int FloorCount { get; set; }
        internal string MapEnterMessage { get; set; }

        internal BlueprintMap(MapInfo info) : base(info, new LootTable())
        {
            EnemyCount = 1;
            FloorCount = 1;
            MapEnterMessage = "nevazhno";
        }
    }
}
