using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class EmptyNode : MapNode
    {
        internal EmptyNode()
        {
            NodeType = NodeType.EMPTY;
            NodeEnterMessage = "Вы пришли в пустую комнату";
        }

        internal override EmptyNode Copy()
        {
            EmptyNode node = new EmptyNode();
            node.NodeEnterMessage = NodeEnterMessage;
            return node;
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }

        internal override void Interact(ExpeditionManager manager)
        {
            throw new NotImplementedException();
        }
    }
}
