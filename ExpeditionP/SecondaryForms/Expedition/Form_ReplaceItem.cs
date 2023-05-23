using ExpeditionP.GameLogic;
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
    public partial class Form_ReplaceItem : Form
    {
        ExpeditionManager Manager { get; init; }
        Item Item { get; set; }

        internal Form_ReplaceItem(ExpeditionManager manager)
        {
            InitializeComponent();
            replaceitem_btn_replace.Text = Constants.replaceitemBtnReplace;
            replaceitem_btn_cancel.Text = Constants.replaceitemBtnCancel;
            Manager = manager;
        }

        internal void LoadForm(Item item)
        {
            Item = item;

            replaceitem_listbox_equippeditems.Items.Clear();
            replaceitem_btn_replace.Enabled = false;
            replaceitem_richtextbox_newitemstats.Clear();
            replaceitem_richtextbox_selecteditemstats.Clear();

            //if (Item.GetType() == typeof(Weapon))
            if (Item is Weapon)
            {
                foreach (Weapon weapon in Manager.GameInstance.Player.EquippedWeapons)
                { 
                    replaceitem_listbox_equippeditems.Items.Add((weapon.Info.Name is null) ? weapon.Info.InternalName : weapon.Info.Name); 
                }
            }
            else
            {
                foreach (Accessory acc in Manager.GameInstance.Player.EquippedAccessories)
                { 
                    replaceitem_listbox_equippeditems.Items.Add((acc.Info.Name is null) ? acc.Info.InternalName : acc.Info.Name); 
                }
            }
            replaceitem_richtextbox_newitemstats.Text = item.GetItemStatsAsString();
            this.TopMost = true;
            this.Show();
        }

        internal void ReplaceItem(int index)
        {
            //if (Item.GetType() == typeof(Weapon))
            if (Item is Weapon)
            {
                Manager.GameInstance.Player.AddWeapon((Weapon)Item, index);
            }
            else
            {
                Manager.GameInstance.Player.AddAccessory((Accessory)Item, index);
            }
            Manager.Form_ItemPick.HideForm();
        }

        private void replaceitem_btn_replace_Click(object sender, EventArgs e)
        {
            if (replaceitem_listbox_equippeditems.SelectedIndex < 0) return;
            ReplaceItem(replaceitem_listbox_equippeditems.SelectedIndex);
        }

        private void replaceitem_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void replaceitem_listbox_equippeditems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = replaceitem_listbox_equippeditems.SelectedIndex;
            if (index < 0) 
            {
                replaceitem_btn_replace.Enabled = false; 
                return; 
            }

            if (Item is Weapon)
            {
                Weapon selectedItem = Manager.GameInstance.Player.EquippedWeapons[index];
                replaceitem_richtextbox_selecteditemstats.Text = selectedItem.GetItemStatsAsString();
            }
            else
            {
                Accessory selectedItem = Manager.GameInstance.Player.EquippedAccessories[index];
                replaceitem_richtextbox_selecteditemstats.Text = selectedItem.GetItemStatsAsString();
            }

            replaceitem_btn_replace.Enabled = true;
        }
    }
}
