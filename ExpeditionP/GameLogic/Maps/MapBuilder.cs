using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps
{
    internal class MapBuilder
    {
        internal MapInfo Info { get; init; }
        List<MapNode> StartingNodes { get; init; }
        internal LootTable LootTable { get; init; }
        internal DifficultySettings DifficultySettings { get; set; }

        internal MapNode? CurrentEditedNode { get; set; }

        internal MapBuilder()
        {
            Info = new MapInfo();
            Info.InternalName = Constants.defaultInternalMapName;
            Info.Name = Constants.defaultInternalMapName;
            Info.ExpeditionTag = Tag.Other;
            StartingNodes = new List<MapNode>();
            LootTable = new LootTable();
            DifficultySettings = new DifficultySettings();
            CurrentEditedNode = null;
        }

        internal void AddStartingNode()
        {
            StartingNodes.Add(new EmptyNode());
            CurrentEditedNode = StartingNodes[StartingNodes.Count - 1];
        }

        internal void AddStartingNode(MapNode existingNode)
        {
            StartingNodes.Add(existingNode);
            CurrentEditedNode = StartingNodes[StartingNodes.Count - 1];
        }

        internal void AddNextNode(MapNode node)
        {
            if (CurrentEditedNode == null)
            {
                Program.SendToLog("[MapBuilder] Не удалось добавить новую ноду в карте " + Info.InternalName +
                    ", т.к. CurrentEditedNode == null");
                return;
            }
            CurrentEditedNode.NextNode = node;
            node.PreviousNode = CurrentEditedNode;
            CurrentEditedNode = node;
        }

        internal void AddItemNodeContent(Item content)
        {
            if (!(CurrentEditedNode is ItemNode))
            {
                Program.SendToLog("[MapBuilder] Использование неверного метода установки контента ноды в карте " + Info.InternalName +
                    ", CurrentEditedNode не является ItemNode");
                return;
            }
            ItemNode itemNode = (ItemNode)CurrentEditedNode;
            if (itemNode.IsPregenerated)
            {
                Program.SendToLog("[MapBuilder] Предупреждение: добавление контента ноды для прегенерированной ItemNode в карте " + Info.InternalName);
            }
            itemNode.Content.Add(content);
        }

        internal void SetEnemyNodeContent(string content)
        {
            if (!(CurrentEditedNode is EnemyNode))
            {
                Program.SendToLog("[MapBuilder] Использование неверного метода установки контента ноды в карте " + Info.InternalName +
                    ", CurrentEditedNode не является EnemyNode");
                return;
            }
            EnemyNode enemyNode = (EnemyNode)CurrentEditedNode;
            if (enemyNode.IsPregenerated)
            {
                Program.SendToLog("[MapBuilder] Предупреждение: добавление контента ноды для прегенерированной EnemyNode в карте " + Info.InternalName);
            }
            enemyNode.Content = content;
        }
        internal void AddItemNodeContent(string content)
        {
            if (!(CurrentEditedNode is ItemNode))
            {
                Program.SendToLog("[MapBuilder] Использование неверного метода установки контента ноды в карте " + Info.InternalName +
                    ", CurrentEditedNode не является ItemNode");
                return;
            }
            ItemNode itemNode = (ItemNode)CurrentEditedNode;
            if (itemNode.IsPregenerated)
            {
                Program.SendToLog("[MapBuilder] Предупреждение: добавление контента ноды для прегенерированной ItemNode в карте " + Info.InternalName);
            }
            Item? toAdd = null;
            try
            {
                toAdd = ItemHolder.RegisteredItems[content];
            }
            catch (KeyNotFoundException)
            {
                Program.SendToLog("[MapBuilder] Ошибка: " + content + " не является существующим предметом");
                return;
            }
            itemNode.Content.Add(toAdd);
        }

        internal void SetNodeEnterMessage(string content)
        {
            if (CurrentEditedNode is not null) CurrentEditedNode.NodeEnterMessage = content;
        }

        internal int GetStartingNodesCount() { return StartingNodes.Count; }

        internal Map ToMap()
        {
            // проверки на завершенность информации
            if (Info.InternalName == Constants.defaultInternalMapName)
                Program.SendToLog("[MapBuilder] Предупреждение. Зарегестрирована карта со стандартным ID");
            if (Info.Name == Constants.defaultInternalMapName)
                Program.SendToLog(String.Format(
                    "[MapBuilder] Предупреждение. Зарегестрирована карта ({0}) со стандартным названием", Info.InternalName));

            Map toReturn = new Map(Info, LootTable);
            toReturn.DifficultySettings.SetDifficulty(DifficultySettings);
            foreach (MapNode node in StartingNodes) toReturn.StartingNodes.Add(node);
            return toReturn;
        }

        internal List<MapNode> ToStartingNodesList()
        {
            return StartingNodes;
        }
    }
}
