using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions
{
    internal abstract class AbstractMap
    {
        internal LootTable LootTable { get; set; } = new LootTable();
        internal abstract Map GetAsMap(MapManager manager);
    }
}
