using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    // Сделано специально для туториала
    internal class SpecialExit1Node : MapNode
    {
        internal SpecialExit1Node()
        {
            NodeType = NodeType.EXIT;
            NodeEnterMessage = String.Empty;
        }

        internal override SpecialExit1Node Copy()
        {
            SpecialExit1Node exitNode = new SpecialExit1Node();
            exitNode.NodeEnterMessage = NodeEnterMessage;
            return exitNode;
        }

        internal override void Interact(ExpeditionManager manager)
        {
            // Позже добавить возможность забирать шмот с карты
            manager.QuitExpedition(false);
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }
    }
}
