using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class RestNode : MapNode
    {
        internal RestNode()
        {
            NodeType = NodeType.REST;
        }

        internal override RestNode Copy()
        {
            return new RestNode();
        }

        internal override void Interact(ExpeditionManager manager)
        {
            manager.Form_RestPick.LoadForm();
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }
    }
}
