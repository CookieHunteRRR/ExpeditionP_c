using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpeditionP.SecondaryForms.Expedition
{
    public partial class Form_ExpeditionResult : Form
    {
        List<Item> PickableItems { get; set; }
        ExpeditionManager? ExpeditionManager { get; set; }

        public Form_ExpeditionResult()
        {
            InitializeComponent();
            PickableItems = new List<Item>();

            expresult_btn_choose.Text = Constants.itempickBtnChoose;
        }

        /// <summary>
        /// Определяет, может ли игрок забрать с экспедиции предмет и сразу же заполняет список предметов, которые игрок может забрать
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal bool PlayerHasPickableItems(Player player)
        {
            PickableItems.Clear();

            var equipment = player.GetAllEquipment();
            foreach (var item in equipment)
            {
                if (Program.Game.GameInstance.CollectedItems.Contains(item.Info.InternalName)) continue;
                PickableItems.Add(item);
            }

            return PickableItems.Count > 0;
        }

        internal void LoadForm(ExpeditionManager manager)
        {
            ExpeditionManager = manager;

            foreach (var item in PickableItems)
            {
                expresult_listbox_itemlist.Items.Add(item.Info.Name);
            }

            this.Show();
        }

        void HideForm()
        {
            ExpeditionManager.ExpeditionForm.ExitExpedition();

            ExpeditionManager = null;
            this.Hide();
        }
        void UpdateItemStatBox(Item selectedItem)
        {
            expresult_richtextbox_itemstats.Clear();
            expresult_richtextbox_itemstats.Text = selectedItem.GetItemStatsAsString();
        }

        void AddPickedItemToPlayer()
        {
            Item selectedItem = PickableItems[expresult_listbox_itemlist.SelectedIndex];
            Program.Game.GameInstance.AddItemToCollectedItems(selectedItem.Info.InternalName);
        }

        private void Form_ExpeditionResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                HideForm();
            }
        }

        private void expresult_btn_choose_Click(object sender, EventArgs e)
        {
            int index = expresult_listbox_itemlist.SelectedIndex;
            //MessageBox.Show("Нафидил с позором", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (index < 0)
            {
                MessageBox.Show("Предмет не выбран", "-", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HideForm();
                return;
            }
            AddPickedItemToPlayer();
            MessageBox.Show($"Выбран {PickableItems[index].Info.Name}", "-", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HideForm();
        }

        private void expresult_listbox_itemlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (expresult_listbox_itemlist.SelectedIndex < 0) { expresult_btn_choose.Enabled = false; return; }

            expresult_btn_choose.Enabled = true;

            Item selectedItem = PickableItems[expresult_listbox_itemlist.SelectedIndex];
            UpdateItemStatBox(selectedItem);
        }
    }
}
