using ExpeditionP.GameLogic.Maps.Expeditions;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps
{
    internal class MapGenerator
    {
        internal Map BuildMap(BlueprintMap bp)
        {
            if (bp.StartingNodes.Count < 1)
            {
                throw new Exception("Карта не может быть сгенерирована, т.к. у нее отсутствуют какие-либо стартовые ноды");
            }

            MapBuilder mb = new MapBuilder();
            mb.Info.InternalName = bp.Info.InternalName;
            mb.Info.Name = bp.Info.Name;
            mb.Info.ExpeditionTag = bp.Info.ExpeditionTag;
            mb.DifficultySettings = bp.DifficultySettings;

            MapNode? prevFloorLastNode = null;
            for (int floor = 0; floor < bp.StartingNodes.Count; floor++) // Проходимся по каждому этажу
            {
                // - -> E -> I -> BP (I E E R I E)
                MapNode? currOriginalNode = bp.StartingNodes[floor];
                mb.AddStartingNode(currOriginalNode.Copy());
                mb.CurrentEditedNode.PreviousNode = prevFloorLastNode;
                currOriginalNode = currOriginalNode.NextNode;
                while (currOriginalNode != null && currOriginalNode.NodeType != NodeType.STAIRS) // Проходимся по этажу пока не дойдем до его конца
                {
                    if (currOriginalNode.NodeType == NodeType.BLUEPRINT)
                    {
                        BuildBlueprintNode(mb, (BlueprintNode)currOriginalNode);
                        currOriginalNode = currOriginalNode.NextNode;
                        continue;
                    }
                    mb.AddNextNode(currOriginalNode.Copy());
                    currOriginalNode = currOriginalNode.NextNode;
                }
                // В этой части кода оказываемся, когда currOriginalNode это нода после выхода, либо это лестница
                // Т.к. нам нужны новые ссылки на ноды для лестницы, устанавливаем их здесь
                if (currOriginalNode == null) // Если мы находимся (предположительно) на ноде после выхода
                {
                    if (mb.CurrentEditedNode.NodeType == NodeType.EXIT)
                    {
                        return mb.ToMap(); // Если действительно выход
                    }
                    // Если же каким-то образом это оказался не выход
                    if (floor + 1 >= bp.StartingNodes.Count) // если это последний этаж
                    {
                        mb.AddNextNode(new ExitNode());
                        return mb.ToMap();
                    }
                    else // создаем лестницу и делаем ее последней нодой, теперь следующая сгенерированная нода на новом этаже будет отсылаться на нее
                    {
                        StairsNode stairsNode = new StairsNode();
                        mb.AddNextNode(stairsNode);
                        prevFloorLastNode = stairsNode;
                    }
                }
            }
            return mb.ToMap();
        }

        // возвращает последнюю сгенерированную ноду
        void BuildBlueprintNode(MapBuilder mb, BlueprintNode bp)
        {
            int enemyNodesToGenerate = bp.EnemyCount;

            //MapNode currentNode = startingPrevNode;
            while (enemyNodesToGenerate > 0)
            {
                MapNode nextNode = GenerateNode(mb.CurrentEditedNode);
                mb.AddNextNode(nextNode);
                if (mb.CurrentEditedNode.NodeType == NodeType.ENEMY) enemyNodesToGenerate--;
            }
        }

        // В этом методе мы отталкиваемся от ноды на этаже - берем 3 предыдущие ноды через PreviousNode
        // lastNode в данном случае [node1, node2, lastNode, nodeToGenerate]
        MapNode GenerateNode(MapNode lastNode)
        {
            int enemyCount = 0, itemCount = 0, restCount = 0;
            for (int i = 0; i < 3; i++)
            {
                switch (lastNode.NodeType)
                {
                    case NodeType.ENEMY:
                        enemyCount++;
                        break;
                    case NodeType.ITEM:
                        itemCount++;
                        break;
                    case NodeType.REST:
                        restCount++;
                        break;
                    default:
                        i--;
                        break;
                }
                if (lastNode.PreviousNode is not null)
                    lastNode = lastNode.PreviousNode;
                else
                    break;
            }

            int[] weights = new int[3];
            weights[0] = GetEnemyNodeChance(enemyCount, itemCount, restCount);
            weights[1] = GetItemNodeChance(enemyCount, itemCount, restCount);
            weights[2] = GetRestNodeChance(enemyCount, itemCount, restCount);
            NodeType typeToGenerate = GenerateNodeType(weights);
            switch (typeToGenerate)
            {
                case NodeType.ENEMY:
                    return new EnemyNode(false);
                case NodeType.ITEM:
                    return new ItemNode(false);
                case NodeType.REST:
                    return new RestNode();
                default:
                    return new EmptyNode();
            }
        }

        NodeType GenerateNodeType(int[] weights)
        {
            List<int> intermediateWeights = new List<int>();
            int totalWeight = 0;
            foreach (int i in weights)
            {
                totalWeight += i;
                intermediateWeights.Add(totalWeight);
            }
            int generatedWeight = Program.Random.Next(totalWeight);
            if (generatedWeight < intermediateWeights[0])
                return NodeType.ENEMY;
            if (generatedWeight < intermediateWeights[1])
                return NodeType.ITEM;
            if (generatedWeight <= intermediateWeights[2])
                return NodeType.REST;
            return NodeType.EMPTY;
        }

        int GetEnemyNodeChance(int enemyCount, int itemCount, int restCount)
        {
            if (enemyCount + restCount + itemCount == 0) return 0;
            if (enemyCount >= 2) return 0;
            if (itemCount >= 2) return 2;
            return 1;
        }

        int GetItemNodeChance(int enemyCount, int itemCount, int restCount)
        {
            if (enemyCount + restCount + itemCount == 0) return 1;
            if (itemCount >= 3) return 0;
            if (restCount >= 1) return 2;
            return 1;
        }

        int GetRestNodeChance(int enemyCount, int itemCount, int restCount)
        {
            if (enemyCount > 0)
            {
                if (enemyCount + restCount + itemCount == 0) return 0;
                if (restCount > 0) return 0;
                if (enemyCount >= 2) return 2;
                return 1;
            }
            return 0;
        }
    }
}
