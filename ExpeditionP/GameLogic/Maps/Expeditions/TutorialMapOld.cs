using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions
{
    internal class TutorialMapOld : AbstractMap
    {
        internal override Map GetAsMap(MapManager manager)
        {
            MapBuilder tutorialMapBuilder = new MapBuilder();
            tutorialMapBuilder.Info.InternalName = "map_other_tutorial";
            tutorialMapBuilder.Info.Name = "Tutorial Map";
            tutorialMapBuilder.Info.ExpeditionTag = Items.Tag.Standart;

            // Не требуется для туториала
            //tutorialMapBuilder.LootTable.AddItemsByExpedition(Items.Tag.Other);

            tutorialMapBuilder.AddStartingNode();
            tutorialMapBuilder.SetNodeEnterMessage("Добро пожаловать в ExpeditionP. " +
                "В данном туториале я постараюсь объяснить вам базовые механики игры. В кратце, цель игры - собрать все предметы. " +
                "Для этого игроку предстоит ходить, собственно, в экспедиции. По завершению каждой экспедиции игрок может выбрать один предмет, " +
                "который заберет с собой в убежище. А может и не забирать ничего. Начнем с объяснения этого \"необычного\" интерфейса. " +
                "Нажмите кнопку \"" + Constants.expeditionBtnMoveText + "\", чтобы перейти в следующую ноду.\n");
            tutorialMapBuilder.AddNextNode(new EmptyNode());
            tutorialMapBuilder.SetNodeEnterMessage("Что за ноды? Каждая экспедиция состоит из нод, иными словами, клеток, " +
                "по которым игрок может ходить. Пустые ноды, вроде этой, могут просто что-то рассказать игроку. " +
                "Следующая (предметная) нода содержит в себе заранее указанный предмет. " +
                "В основном, в экспедициях вам будут попадаться сгенерированные предметы.\n");
            tutorialMapBuilder.AddNextNode(new ItemNode(true));
            tutorialMapBuilder.SetNodeEnterMessage("Предметная нода, как я уже сказал, содержит предметы. " +
                "Предметы в данной игре - детали, из которых вы должны собрать какой-нибудь билд. Типов предметов всего 2 - оружие и аксессуары. " +
                "Оружие, в случае данного примера - Сломанный меч, зачастую основной предмет (или несколько предметов?), " +
                "вокруг которого строится билд. К примеру, это оружие - меч с физическим уроном. Значит вам понадобятся аксессуары, " +
                "увеличивающие физический урон и криты. Помимо, разумеется, аксессуаров на выживаемость. Но обо всем по порядку. " +
                "Чтобы взять предмет, вы должны нажать кнопку \"" + Constants.expeditionBtnInteractText + "\", которая позволит " +
                "взаимодействовать с любой нодой, с которой можно взаимодействовать.\n");
            tutorialMapBuilder.AddItemNodeContent(ItemHolder.RegisteredItems["weapon_brokensword"]);
            tutorialMapBuilder.AddNextNode(new ItemNode(true));
            tutorialMapBuilder.SetNodeEnterMessage("Эта предметная нода содержит аксессуар, в данном примере - Полицейский шлем. " +
                "Это один из аксессуаров на выживаемость. По мере развития игры будут добавляться не только банальные +10 здоровья, " +
                "+10 брони, но на данный момент все довольно простенько. Следующая нода будет вражеской.\n");
            tutorialMapBuilder.AddItemNodeContent(ItemHolder.RegisteredItems["acc_policehelmet"]);
            tutorialMapBuilder.AddNextNode(new EnemyNode(false));
            tutorialMapBuilder.SetEnemyNodeContent("mob_standart_zombie");
            tutorialMapBuilder.AddNextNode(new ExitNode());

            return tutorialMapBuilder.ToMap();
        }
    }
}
