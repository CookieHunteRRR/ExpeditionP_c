using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class BlueprintNode : MapNode
    {
        internal int EnemyCount { get; set; }

        internal BlueprintNode(int enemyCount)
        {
            NodeType = NodeType.BLUEPRINT;
            EnemyCount = (enemyCount > 0) ? enemyCount : 1;
        }

        internal override MapNode Copy()
        {
            throw new NotImplementedException();
        }

        internal override void Interact(ExpeditionManager manager)
        {
            throw new NotImplementedException();
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }
    }
}
