using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal abstract class MapNode
    {
        internal MapNode? PreviousNode { get; set; }
        internal MapNode? NextNode { get; set; }
        internal NodeType NodeType { get; init; }
        internal string? NodeEnterMessage { get; set; }

        internal abstract void ExecuteNodeEnterLogic(ExpeditionManager manager);
        internal abstract void Interact(ExpeditionManager manager);

        // На будущее почему я не сделал дефолтную реализацию которую развивают дальше наследующие классы
        // Вот скопирую я чистую мапноду без всяких типов, а приведя ее к условной ItemNode как я мапноду превращу в итемноду
        internal abstract MapNode Copy();
    }
}
