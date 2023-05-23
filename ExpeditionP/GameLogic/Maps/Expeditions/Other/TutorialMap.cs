using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Expeditions.Other
{
    internal class TutorialMap : BlueprintMap
    {
        internal TutorialMap() : base(new MapInfo("map_tutorial", "Туториал", null, Items.Tag.Other))
        {
            DifficultySettings.SetDifficulty(1, 0, 1);

            EnemyCount = 0;
            FloorCount = 1;
            MapEnterMessage = String.Empty;

            MapBuilder mb = new MapBuilder();
            mb.AddStartingNode();
            mb.SetNodeEnterMessage($"Добро пожаловать в (недоделанный) туториал ExpeditionP. Чтобы продвигаться по карте, нажимайте кнопку \"{Constants.expeditionBtnMoveText}\"");

            mb.AddNextNode(new EmptyNode());
            string s1 = $"\nВ кратце, суть игры (или скорее похода в экспедицию) - дойти до конца экспедиции живым. " +
                $"Задача не самая легкая, потому что баланс в этой игре говно, но я тестил, это сделать возможно.";
            mb.SetNodeEnterMessage(s1);

            mb.AddNextNode(new EmptyNode());
            string s2 = $"\nСобственно, чтобы повысить свои шансы на выживание - вам нужны предметы. Всего их два типа - оружие и аксессуары. "
                + "Оружие - ваш главный источник урона, в то время как аксессуары - либо дальнейшее усиление этого урона, либо увеличение вашей защиты.";
            mb.SetNodeEnterMessage(s2);

            mb.AddNextNode(new EmptyNode());
            string s3 = $"\nСоответственно, в первую очередь вам нужно обзавестись оружием, а уже затем начинать собирать аксессуары. " +
                $"Сделаете наоборот - пеняйте на себя. " +
                $"Следующая нода будет \"предметной\" и там появится применение следующей кнопке - \"{Constants.expeditionBtnInteractText}\"";
            mb.SetNodeEnterMessage(s3);

            mb.AddNextNode(new ItemNode(true));
            Weapon weaponForTutorial = (Weapon)ItemHolder.RegisteredItems["weapon_axe"];
            string s4 = $"\nПри попадании на ноду вы не сразу же попадаете в бой или выбираете предмет для подбора. " +
                $"Сначала вам нужно нажать упомянутую ранее кнопку. Я не знаю зачем я сделал именно так, но менять мне это не хочется. " +
                $"В любом случае, сейчас у нас речь про оружие. " +
                $"Оружие имеет минимальный и максимальный урон, соответственно изначальный урон будет в этом диапазоне. " +
                $"Затем он изменяется в зависимости от ваших статов, защиты врага, все как во всех играх, что тут объяснять то. " +
                $"Некоторое оружие также может иметь интересные эффекты, в таком случае описание подобного оружия вас проинформирует. " +
                $"А, ну и если вы когда нибудь забудете статы вашего подобранного предмета (оружия либо аксессуара), просто наведитесь на него " +
                $"в инвентаре справа\n\n" +
                $"Так вот, при взаимодействии с нодой открывается интерфейс выбора предмета. Нажмите на интересующий вас предмет и возьмите его к себе " +
                $"в инвентарь кнопкой \"{Constants.itempickBtnChoose}\". Если вас не интересует ни один предмет, вы также можете нажать кнопку " +
                $"\"{Constants.itempickBtnSkip}\"";
            mb.SetNodeEnterMessage(s4);
            mb.AddItemNodeContent(weaponForTutorial);

            mb.AddNextNode(new EnemyNode(true));
            string s5 = $"\nПодобрав оружие, его нужно экипировать. Для этого выделите нужное оружие и нажмите кнопку \"{Constants.expeditionBtnEquipWeaponText}\"" +
                $"\n\nНаконец - боевка. Здесь все (на данный момент) элементарно - спамите кнопку \"{Constants.expeditionBtnAttackText}\", " +
                $"пока противник не умрет. Ну или вы. Такая вот пока что игра.";
            mb.SetNodeEnterMessage(s5);
            mb.SetEnemyNodeContent("mob_standart_zombie");

            mb.AddNextNode(new SpecialExit1Node());
            string s6 = "\nВсе, не информативный туториал сделал, можно отдыхать." +
                "\nАх, да, по завершению экспедиции вам предложат забрать любой имеющийся у вас предмет с собой. " +
                "Один из предметов, забранных с собой, можно будет взять с собой в экспедицию, выбрав его на форме с выбором экспедиции. " +
                "Однако отсюда вы ничего не заберете, потому что это туториал. Удачи!";
            mb.SetNodeEnterMessage(s6);

            StartingNodes = mb.ToStartingNodesList();
        }
    }
}
