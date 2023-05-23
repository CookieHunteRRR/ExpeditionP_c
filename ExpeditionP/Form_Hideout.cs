using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps;
using ExpeditionP.GameLogic.Maps.Expeditions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpeditionP
{
    public partial class Form_Hideout : Form
    {
        Dictionary<int, string> AvailableMaps { get; init; }
        Item? PreselectedItem { get; set; }

        public Form_Hideout()
        {
            InitializeComponent();
            AvailableMaps = new Dictionary<int, string>();
            PreselectedItem = null;
        }

        void UpdateAvailableMaps()
        {
            MapManager mapManager = Program.Game.GameManager.MapManager;

            AvailableMaps.Clear();
            hideout_listbox_availablemaps.Items.Clear();

            int counter = 0;
            foreach (string id in mapManager.LoadedMaps.Keys)
            {
                AvailableMaps.Add(counter, id);
                counter++;
            }
            foreach (string id in AvailableMaps.Values)
            {
                hideout_listbox_availablemaps.Items.Add(mapManager.LoadedMaps[id].Info.Name);
            }
        }

        void UpdateCollectedItems()
        {
            hideout_listbox_availableitems.Items.Clear();
            hideout_listbox_availableitems.Items.Add("- Не выбирать -");

            foreach (var itemId in Program.Game.GameInstance.CollectedItems)
            {
                Item item = ItemHolder.RegisteredItems[itemId];
                hideout_listbox_availableitems.Items.Add(item.Info.Name);
            }
        }

        void UpdatePreselectedItemName()
        {
            if (PreselectedItem is null)
                hideout_lbl_pickeditemname.Text = Constants.noEquippedItemInHideoutText;
            else
                hideout_lbl_pickeditemname.Text = PreselectedItem.Info.Name;
        }

        void AddPreselectedItemToPlayer()
        {
            if (PreselectedItem is null) return;

            Player player = Program.Game.GameInstance.Player;
            if (PreselectedItem is Weapon) player.AddWeapon((Weapon)PreselectedItem);
            else player.AddAccessory((Accessory)PreselectedItem);
        }

        public void LoadHideout()
        {
            UpdateAvailableMaps();
            UpdateCollectedItems();
            UpdatePreselectedItemName();
            this.Show();
        }

        private void hideout_btn_launchmap_Click(object sender, EventArgs e)
        {
            if (hideout_listbox_availablemaps.SelectedIndex < 0) return;
            Map map = Program.Game.GameManager.MapManager.LoadedMaps[AvailableMaps[hideout_listbox_availablemaps.SelectedIndex]];
            AddPreselectedItemToPlayer();
            if (map is BlueprintMap)
            {
                BlueprintMap bpmap = (BlueprintMap)map;
                Map generatedMap = Program.Game.GameManager.MapManager.MapGenerator.BuildMap(bpmap);
                Program.Expedition.LoadExpedition(generatedMap);
            }
            else
            {
                Program.Expedition.LoadExpedition(map);
            }
            this.Hide();
        }

        private void Form_Hideout_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void hideout_btn_selectitem_Click(object sender, EventArgs e)
        {
            int index = hideout_listbox_availableitems.SelectedIndex;
            if (index > 0)
            {
                string itemId = Program.Game.GameInstance.CollectedItems[index - 1];
                Item toSet = ItemHolder.RegisteredItems[itemId];
                PreselectedItem = toSet;
            }
            else PreselectedItem = null;
            UpdatePreselectedItemName();
        }
    }
}
