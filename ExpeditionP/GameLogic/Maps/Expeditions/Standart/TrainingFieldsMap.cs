using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions.Standart
{
    internal class TrainingFieldsMap : BlueprintMap
    {
        internal TrainingFieldsMap() : base(new MapInfo("map_standart_trainingfields", "Тренировочные поля", null, Items.Tag.Standart))
        {
            MapBuilder mb = new MapBuilder();
            mb.Info.InternalName = "map_standart_trainingfields";
            mb.Info.Name = "Тренировочные поля";
            mb.Info.ExpeditionTag = Items.Tag.Standart;

            // 2I - 1E - 1I - 2E x3

            mb.AddStartingNode();
            mb.SetNodeEnterMessage("Вы оказались на тренировочных полях");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("weapon_gisla");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("acc_glasscannon");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("acc_policekevlar");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("acc_policearmorset");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("acc_vampiricfang");
            mb.AddNextNode(new ItemNode(true));
            mb.AddItemNodeContent("acc_ringofpower");
            mb.AddNextNode(new EnemyNode(true));
            mb.SetEnemyNodeContent("boss_standart_headbandit");
            mb.AddNextNode(new BlueprintNode(10));
            mb.AddNextNode(new ExitNode());
            StartingNodes = mb.ToStartingNodesList();
        }
    }
}
