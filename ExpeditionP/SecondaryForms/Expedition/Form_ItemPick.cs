using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Items.Instances.Consumables;
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
    internal partial class Form_ItemPick : Form
    {
        ExpeditionManager Manager { get; set; }
        List<Item> Items { get; set; }

        internal Form_ItemPick(ExpeditionManager manager)
        {
            InitializeComponent();
            itempick_btn_choose.Text = Constants.itempickBtnChoose;
            itempick_btn_skip.Text = Constants.itempickBtnSkip;
            Manager = manager;
        }

        internal void LoadForm(List<Item> items)
        {
            Items = items;

            itempick_listbox_itemlist.Items.Clear();
            itempick_btn_choose.Enabled = false;
            itempick_richtextbox_itemstats.Clear();

            foreach (Item item in Items)
            {
                string name = (item.Info.Name is null) ? item.Info.InternalName : item.Info.Name;
                itempick_listbox_itemlist.Items.Add(name);
            }

            this.TopMost = true;
            this.Show();
        }

        void UpdateItemStatBox(Item selectedItem)
        {
            itempick_richtextbox_itemstats.Clear();
            itempick_richtextbox_itemstats.Text = selectedItem.GetItemStatsAsString();
        }

        internal void HideForm()
        {
            Manager.ExpeditionForm.SetMoveBtnAvailability(true);
            this.Hide();
            Manager.Form_ReplaceItem.Hide();
            Manager.ExpeditionForm.UpdateAll();
        }

        private void itempick_listbox_itemlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itempick_listbox_itemlist.SelectedIndex < 0) { itempick_btn_choose.Enabled = false; return; }

            itempick_btn_choose.Enabled = true;

            Item selectedItem = Items[itempick_listbox_itemlist.SelectedIndex];
            UpdateItemStatBox(selectedItem);
        }

        private void itempick_btn_choose_Click(object sender, EventArgs e)
        {
            Item selectedItem = Items[itempick_listbox_itemlist.SelectedIndex];

            if (selectedItem is Weapon)
            {
                Weapon weapon = (Weapon)selectedItem;
                // Если у игрока есть место
                if (Manager.GameInstance.Player.EquippedWeapons.Count < Constants.maximumEquippedWeapons)
                {
                    Manager.GameInstance.Player.AddWeapon(weapon);
                    HideForm();
                    return;
                }
                // Если места нет
                Manager.Form_ReplaceItem.LoadForm(weapon);
            }
            else if (selectedItem is Accessory)
            {
                Accessory acc = (Accessory)selectedItem;
                if (Manager.GameInstance.Player.EquippedAccessories.Count < Constants.maximumEquippedAccessories)
                {
                    Manager.GameInstance.Player.AddAccessory(acc);
                    HideForm();
                    return;
                }
                Manager.Form_ReplaceItem.LoadForm(acc);
            }
            else if (selectedItem is Consumable)
            {
                Consumable consumable = (Consumable)selectedItem;
                consumable.Consume(Manager);
                HideForm();
                return;
            }
            else
            {
                throw new Exception("Для типа выбранного предмета нет реализации подбора");
            }
        }

        private void itempick_btn_skip_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void Form_ItemPick_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }
    }
}
