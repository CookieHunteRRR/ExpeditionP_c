using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class ExitNode : MapNode
    {
        internal ExitNode()
        {
            NodeType = NodeType.EXIT;
            NodeEnterMessage = "Вы пришли к выходу";
        }

        internal override ExitNode Copy()
        {
            ExitNode exitNode = new ExitNode();
            exitNode.NodeEnterMessage = NodeEnterMessage;
            return exitNode;
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }

        internal override void Interact(ExpeditionManager manager)
        {
            // Позже добавить возможность забирать шмот с карты
            manager.QuitExpedition(true);
        }
    }
}