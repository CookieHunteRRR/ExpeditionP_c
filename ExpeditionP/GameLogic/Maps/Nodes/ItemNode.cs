using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using ExpeditionP.SecondaryForms.Expedition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps.Nodes
{
    internal class ItemNode : MapNode
    {
        internal bool IsPregenerated { get; init; }
        internal List<Item> Content { get; set; }

        readonly string itemsFoundMsg = "Вы нашли предметы";
        readonly string itemsNotFoundMsg = "Вы не нашли ничего стоящего вашего внимания";

        internal ItemNode(bool isPregenerated)
        {
            NodeType = NodeType.ITEM;
            IsPregenerated = isPregenerated;
            Content = new List<Item>();
        }

        // Метод, генерирующий шмотки. Идет отдельно, потому что экспедишн менеджер возможно передать только если
        // игра уже запущена. Очевидно, что при загрузке всех карт экспедиции никакой нет
        // Да и в принципе логично что надо генерировать при входе в клетку, чтобы актуальный шмот учитывался
        internal void GenerateContent(ExpeditionManager manager)
        {
            Content = manager.GenerateItemsForItemNode();
        }

        internal override void Interact(ExpeditionManager manager)
        {
            if (!IsPregenerated) GenerateContent(manager);
            if (Content.Count > 0)
            {
                manager.SendToLog(itemsFoundMsg);
                manager.Form_ItemPick.LoadForm(Content);
            }
            else
            {
                manager.SendToLog(itemsNotFoundMsg);
                manager.ExpeditionForm.SetMoveBtnAvailability(true);
            }
        }

        internal override ItemNode Copy()
        {
            ItemNode node = new ItemNode(IsPregenerated);
            node.NodeEnterMessage = NodeEnterMessage;
            node.Content = new List<Item>(Content);
            return node;
        }

        internal override void ExecuteNodeEnterLogic(ExpeditionManager manager) { }
    }
}
