using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class StairsNode : MapNode
    {
        internal override StairsNode Copy()
        {
            return new StairsNode();
        }

        internal override void Interact(ExpeditionManager manager)
        {
            throw new NotImplementedException();
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }
    }
}
