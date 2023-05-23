using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps;
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
    internal partial class Form_RestPick : Form
    {
        ExpeditionManager Manager { get; set; }
        List<RestAction> RestActions { get; set; } // список возможных действий: гарантирован хил, а потом идут возможные действия с реликами

        internal Form_RestPick(ExpeditionManager manager)
        {
            InitializeComponent();
            RestActions = new List<RestAction>();
            restpick_btn_choose.Text = Constants.itempickBtnChoose;
            restpick_btn_skip.Text = Constants.itempickBtnSkip;
            Manager = manager;
        }

        internal void LoadForm()
        {
            restpick_btn_choose.Enabled = false;
            restpick_listbox_actionlist.Items.Clear();
            restpick_txtbox_actioninfo.Text = String.Empty;

            LoadRestActions();
            this.TopMost = true;
            this.Show();
        }

        // Генерирует действия на основе экипированных предметов, проверяя есть ли там релики
        void LoadRestActions()
        {
            RestActions.Clear();

            RestActions.Add(RestAction.Heal);

            UpdateActionListbox();
        }

        void UpdateActionListbox()
        {
            restpick_listbox_actionlist.Items.Clear();
            foreach (var action in RestActions)
                restpick_listbox_actionlist.Items.Add(GetActionNameForListbox(action));
        }

        string GetActionNameForListbox(RestAction action)
        {
            switch (action)
            {
                case RestAction.Heal:
                    return "Отдых";
            }
            return "???";
        }

        void UpdateActionInfoText(int index)
        {
            restpick_txtbox_actioninfo.Clear();
            switch (RestActions[index])
            {
                case RestAction.Heal:
                    restpick_txtbox_actioninfo.Text = "Отдохнуть для восстановления 10% максимального здоровья";
                    return;
            }
        }

        internal void HideForm()
        {
            Manager.ExpeditionForm.SetMoveBtnAvailability(true);
            this.Hide();
            Manager.ExpeditionForm.UpdateAll();
        }

        void DoAction(RestAction action)
        {
            switch (action)
            {
                case RestAction.Heal:
                    Player player = Manager.GameInstance.Player;
                    var toHeal = Utils.Round(player.BattleStats.CurrentEntityStats.Health * 0.1);
                    player.ChangeCurrentHealth(toHeal, Manager);
                    Manager.SendToLog("Вы отдохнули и восстановили " + toHeal + " здоровья");
                    return;
            }
        }

        private void restpick_listbox_actionlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (restpick_listbox_actionlist.SelectedIndex < 0) 
            {
                restpick_btn_choose.Enabled = false; 
                return; 
            }

            restpick_btn_choose.Enabled = true;
            UpdateActionInfoText(restpick_listbox_actionlist.SelectedIndex);
        }

        private void restpick_btn_choose_Click(object sender, EventArgs e)
        {
            int index = restpick_listbox_actionlist.SelectedIndex;
            DoAction(RestActions[index]);
            HideForm();
        }

        private void restpick_btn_skip_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void Form_RestPick_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }
    }
}
