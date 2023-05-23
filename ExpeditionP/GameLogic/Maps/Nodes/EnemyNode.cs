using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class EnemyNode : MapNode
    {
        internal bool IsPregenerated { get; init; }
        internal string? Content { get; set; }

        internal EnemyNode(bool isPregenerated)
        {
            NodeType = NodeType.ENEMY;
            NodeEnterMessage = Constants.defaultEnemyEncounterMessage;
            IsPregenerated = isPregenerated;
            Content = null;
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager)
        {
            if (!IsPregenerated) GenerateContent(manager);

            var mob = (Mob)EntityHolder.RegisteredEntities[Content];
            NodeEnterMessage = mob.EncounterMessage;
        }

        internal void GenerateContent(ExpeditionManager manager)
        {
            Tag expeditionTag = (Tag)manager.CurrentMap.Info.ExpeditionTag;
            Content = manager.GenerateMobId(expeditionTag, false); // временно
        }

        internal override void Interact(ExpeditionManager manager)
        {
            manager.ExpeditionForm.LoadBattle(Content);
        }

        internal override EnemyNode Copy()
        {
            EnemyNode node = new EnemyNode(IsPregenerated);
            node.NodeEnterMessage = NodeEnterMessage;
            node.Content = Content;
            return node;
        }
    }
}
