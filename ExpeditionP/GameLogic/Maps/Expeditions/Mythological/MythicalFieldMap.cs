using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions.Mythological
{
    internal class MythicalFieldMap : AbstractMap
    {
        internal override Map GetAsMap(MapManager manager)
        {
            MapBuilder mb = new MapBuilder();
            mb.Info.InternalName = "map_myth_field";
            mb.Info.Name = "Mythical Field";
            mb.Info.ExpeditionTag = Items.Tag.Mythological;

            mb.AddStartingNode();
            mb.SetNodeEnterMessage("Вы оказались в мифическом лесу");
            mb.AddNextNode(new ItemNode(false));
            mb.AddNextNode(new ItemNode(false));
            mb.AddNextNode(new EnemyNode(true));
            mb.SetEnemyNodeContent("mob_standart_zombie");
            mb.AddNextNode(new ExitNode());

            return mb.ToMap();
        }
    }
}
