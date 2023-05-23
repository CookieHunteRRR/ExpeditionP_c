using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Items.Instances.Accessories;
using ExpeditionP.GameLogic.Managers;
using ExpeditionP.GameLogic.Maps;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Action = ExpeditionP.GameLogic.Action;

namespace ExpeditionP
{
    public partial class Form_Expedition : Form
    {
        internal ExpeditionManager? ExpeditionManager { get; set; }
        private ToolTip toolTip;

        public Form_Expedition()
        {
            InitializeComponent();
            expedition_btn_move.Text = Constants.expeditionBtnMoveText;
            expedition_btn_interact.Text = Constants.expeditionBtnInteractText;
            expedition_btn_equip.Text = Constants.unavailableText;
            expedition_btn_useacc.Text = Constants.unavailableText;
            expedition_btn_attack.Text = Constants.expeditionBtnAttackText;
            expedition_btn_flee.Text = Constants.expeditionBtnFleeText;

            toolTip = new ToolTip();
        }

        internal void LoadExpedition(Map map)
        {
            expedition_btn_move.Enabled = true;
            expedition_btn_interact.Enabled = false;
            expedition_btn_equip.Enabled = false;
            expedition_btn_useacc.Enabled = false;
            SetBattleControlsVisibility(false);
            expedition_richtextbox_log.Clear();

            ExpeditionManager = new ExpeditionManager(this, map, Program.Game.GameInstance.GetInstanceForExpedition());
            if (ExpeditionManager.CurrentNode.NodeEnterMessage != null)
                SendToLog(ExpeditionManager.CurrentNode.NodeEnterMessage);
            UpdateAll();
            this.Show();
        }

        internal void LoadBattle(string enemyId)
        {
            ExpeditionManager.BattleManager = new BattleManager(ExpeditionManager, enemyId);
            UpdateEnemyInfo();
            ExpeditionManager.BattleManager.MakeEnemyTurn();
            SendToLog("=== Начало боя ===");
        }

        internal void QuitBattle(BattleFinishReason battleFinishReason)
        {
            switch (battleFinishReason)
            {
                case BattleFinishReason.EnemyDied:
                    ExpeditionManager.ExpeditionForm.SetBattleControlsVisibility(false);
                    ExpeditionManager.BattleManager = null;
                    ExpeditionManager.IncreaseDifficulty();
                    UpdatePlayerStats();
                    break;
                case BattleFinishReason.PlayerFled:
                    SendToLog("Вы успешно сбежали от противника");
                    ExpeditionManager.ExpeditionForm.SetBattleControlsVisibility(false);
                    ExpeditionManager.BattleManager = null;
                    ExpeditionManager.IncreaseDifficulty();
                    UpdatePlayerStats();
                    break;
                case BattleFinishReason.PlayerDied:
                    MessageBox.Show("Помер", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExitExpedition();
                    break;
            }
        }

        internal void ExitExpedition()
        {
            ExpeditionManager.GameInstance.Player.ClearEquipment();
            ExpeditionManager.BattleManager = null;
            ExpeditionManager = null;
            this.Hide();
            Program.Hideout.LoadHideout();
        }

        internal void SendToLog(string message)
        {
            expedition_richtextbox_log.AddString(message);
        }

        internal void SetMoveBtnAvailability(bool state) { expedition_btn_move.Enabled = state; }
        internal void SetFleeBtnAvailability(bool state) { expedition_btn_flee.Enabled = state; }
        internal void SetBattleControlsVisibility(bool state) { expedition_pnl_battle.Visible = state; }
        internal void SetBattleControlsAvailability(bool state)
        {
            expedition_btn_attack.Enabled = state;
            if (expedition_listbox_weapons.SelectedIndex >= 0) expedition_btn_equip.Enabled = state;
        }

        internal void UpdateAll()
        {
            UpdatePlayerStats();
            UpdatePlayerWeapons();
            UpdatePlayerAccessories();
            if (ExpeditionManager.BattleManager is not null) UpdateEnemyInfo();

            // проверочка
            expedition_listbox_effects.SelectedIndex = -1;
            expedition_listbox_enemyeffects.SelectedIndex = -1;
        }

        internal void UpdatePlayerStats()
        {
            // Обновляем текст со статами перса
            expedition_richtextbox_userstats.Clear();
            Player player = ExpeditionManager.GameInstance.Player;

            StringBuilder sb = new StringBuilder();
            sb.Append($"{player.Name}\r\n\n");
            sb.Append($"Здоровье: {player.BattleStats.CurrentHealth}/{player.BattleStats.CurrentEntityStats.Health}\r\n");
            //sb.Append($"Mana: {player.BattleStats.CurrentMana}/{player.BattleStats.CurrentEntityStats.Mana}\r\n");

            var currentStats = player.BattleStats.CurrentEntityStats;
            // Физ статы
            sb.Append($"\nЗащита: {currentStats.Defense}\r\n");
            sb.Append($"Уворот: {Utils.GetBeautifulChanceDisplayText(currentStats.Evasion)}\r\n");
            sb.Append($"Шанс крит. удара: {Utils.GetBeautifulChanceDisplayText(currentStats.CritChance)}\r\n");
            sb.Append($"Критический урон: {currentStats.CritDamage}%\r\n");
            // Маг статы
            /*sb.Append($"\r\nAmplification: {currentStats.Amplification}\r\n");
            sb.Append($"Mitigation: {currentStats.Mitigation}\r\n");
            sb.Append($"Annulment: {Utils.GetBeautifulChanceDisplayText(currentStats.Annulment)}");*/

            expedition_richtextbox_userstats.Text = sb.ToString();

            // Обновляем список эффектов
            expedition_listbox_effects.Items.Clear();
            foreach (var effect in player.BattleStats.CurrentEffects)
            {
                if (!effect.IsHidden) expedition_listbox_effects.Items.Add(effect.Name);
            }

            expedition_richtextbox_userstats.Update();
            expedition_listbox_effects.Update();
        }

        internal void UpdateEnemyInfo()
        {
            // Обновляем текст со статами врага
            expedition_richtextbox_enemystats.Clear();
            Entity enemy = ExpeditionManager.BattleManager.Enemy;
            expedition_richtextbox_enemystats.Text += enemy.GetName() + "\r\n\n";

            expedition_richtextbox_enemystats.Text += "Health: " + ExpeditionManager.BattleManager.Enemy.BattleStats.CurrentHealth +
                "/" + ExpeditionManager.BattleManager.Enemy.BattleStats.CurrentEntityStats.Health + "\r\n";

            // Обновляем список эффектов
            expedition_listbox_enemyeffects.Items.Clear();
            foreach (var effect in enemy.BattleStats.CurrentEffects)
            {
                if (!effect.IsHidden) expedition_listbox_enemyeffects.Items.Add(effect.Name);
            }

            expedition_richtextbox_enemystats.Update();
            expedition_listbox_enemyeffects.Update();
        }

        internal void UpdatePlayerWeapons()
        {
            expedition_listbox_weapons.SelectedIndex = -1;
            expedition_btn_equip.Enabled = false;
            expedition_btn_equip.Text = Constants.unavailableText;

            expedition_listbox_weapons.Items.Clear();
            Player player = ExpeditionManager.GameInstance.Player;
            foreach (Weapon weapon in player.EquippedWeapons)
            {
                string temp = (weapon != player.CurrentEquippedWeapon) ? weapon.Info.Name : $"[ {weapon.Info.Name} ]";
                expedition_listbox_weapons.Items.Add(temp);
            }
        }

        internal void UpdatePlayerAccessories()
        {
            expedition_listbox_accs.SelectedIndex = -1;
            expedition_btn_useacc.Enabled = false;
            expedition_btn_useacc.Text = Constants.unavailableText;

            expedition_listbox_accs.Items.Clear();
            foreach (Accessory acc in ExpeditionManager.GameInstance.Player.EquippedAccessories)
            {
                expedition_listbox_accs.Items.Add(acc.Info.Name);
            }
        }

        private void expedition_btn_move_Click(object sender, EventArgs e)
        {
            ExpeditionManager.ReducePlayerCooldowns();
            ExpeditionManager.ReducePlayerEffectDurations();

            MapNode currentNode = ExpeditionManager.CurrentNode;
            if (currentNode.NextNode == null)
            {
                if (currentNode.NodeType != NodeType.STAIRS && currentNode.NodeType != NodeType.EXIT)
                {
                    Program.Log.AddLine("Карта " + ExpeditionManager.CurrentMap.Info.InternalName + " не имеет нормального выхода");
                    return;
                }
                return;
            }
            ExpeditionManager.CurrentNode = currentNode.NextNode;
            if (ExpeditionManager.CurrentNode.NodeType != NodeType.EMPTY)
            {
                expedition_btn_interact.Enabled = true;
                expedition_btn_move.Enabled = false;
            }
            ExpeditionManager.CurrentNode.ExecuteNodeEnterLogic(ExpeditionManager);
            if (ExpeditionManager.CurrentNode.NodeEnterMessage != null)
                SendToLog(ExpeditionManager.CurrentNode.NodeEnterMessage);
        }

        private void expedition_btn_interact_Click(object sender, EventArgs e)
        {
            // Как то надо исправить тот факт что с клеткой можно интеракт прожать и уйти на другую клетку
            expedition_btn_interact.Enabled = false;
            expedition_btn_move.Enabled = false;
            ExpeditionManager.CurrentNode.Interact(ExpeditionManager);
        }

        private void expedition_btn_attack_Click(object sender, EventArgs e)
        {
            var action = new Action(ActionType.Attack, 1);
            ExpeditionManager.BattleManager.MakePlayerTurn(action);
        }

        private void expedition_btn_equip_Click(object sender, EventArgs e)
        {
            if (expedition_listbox_weapons.SelectedIndex < 0)
            {
                expedition_btn_equip.Enabled = false;
                return;
            }

            if (ExpeditionManager.BattleManager is not null)
            {
                expedition_btn_equip.Enabled = false;
            }

            Player player = ExpeditionManager.GameInstance.Player;
            Weapon selectedWeapon = player.EquippedWeapons[expedition_listbox_weapons.SelectedIndex];
            bool isInBattle = ExpeditionManager.BattleManager is not null;

            if (player.CurrentEquippedWeapon == Constants.GetFistsWeapon())
            {
                player.EquipWeapon(expedition_listbox_weapons.SelectedIndex, ExpeditionManager);
                if (isInBattle) ExpeditionManager.BattleManager.wasWeaponEquippedThisTurn = true;
                SendToLog("Вы взяли " + player.CurrentEquippedWeapon.Info.Name + " в руки");
            }
            else
            {
                // Если игрок выделил экипированное оружие
                if (selectedWeapon == player.CurrentEquippedWeapon)
                {
                    SendToLog("Вы убрали " + player.CurrentEquippedWeapon.Info.Name + " из рук");
                    player.UnequipWeapon(ExpeditionManager);
                    if (isInBattle) ExpeditionManager.BattleManager.wasWeaponUnequippedThisTurn = true;
                    
                }
                // Если у игрока экипировано оружие и выделено не экипированное 
                else
                {
                    SendToLog("Вы заменили " + player.CurrentEquippedWeapon.Info.Name + " на " + selectedWeapon.Info.Name);
                    player.UnequipWeapon(ExpeditionManager);
                    player.EquipWeapon(expedition_listbox_weapons.SelectedIndex, ExpeditionManager);
                    if (isInBattle)
                    {
                        ExpeditionManager.BattleManager.wasWeaponEquippedThisTurn = true;
                        ExpeditionManager.BattleManager.wasWeaponUnequippedThisTurn = true;
                    }
                }
            }

            UpdatePlayerStats();
            UpdatePlayerWeapons();
        }

        private void expedition_listbox_weapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (expedition_listbox_weapons.SelectedIndex < 0) 
            {
                expedition_btn_equip.Text = Constants.unavailableText;
                expedition_btn_equip.Enabled = false;
                return;
            }

            Player player = ExpeditionManager.GameInstance.Player;
            Weapon selectedWeapon = player.EquippedWeapons[expedition_listbox_weapons.SelectedIndex];
            bool isInBattle = ExpeditionManager.BattleManager is not null;

            // Если у игрока не экипировано оружие
            if (player.CurrentEquippedWeapon == Constants.GetFistsWeapon())
            {
                UpdateEquipButtonState("equip", isInBattle);
            }
            else
            {
                // Если игрок выделил экипированное оружие
                if (selectedWeapon == player.CurrentEquippedWeapon)
                    UpdateEquipButtonState("unequip", isInBattle);
                // Если у игрока экипировано оружие и выделено не экипированное 
                else UpdateEquipButtonState("swap", isInBattle);
            }
        }

        void UpdateEquipButtonState(string action, bool isInBattle)
        {
            expedition_btn_equip.Enabled = false;
            switch (action)
            {
                case "equip":
                    expedition_btn_equip.Text = Constants.expeditionBtnEquipWeaponText;
                    if (isInBattle)
                    {
                        if (!ExpeditionManager.BattleManager.wasWeaponEquippedThisTurn) expedition_btn_equip.Enabled = true;
                    }
                    else expedition_btn_equip.Enabled = true;
                    return;
                case "unequip":
                    expedition_btn_equip.Text = Constants.expeditionBtnUnequipWeaponText;
                    if (isInBattle)
                    {
                        if (!ExpeditionManager.BattleManager.wasWeaponUnequippedThisTurn) expedition_btn_equip.Enabled = true;
                    }
                    else expedition_btn_equip.Enabled = true;
                    return;
                case "swap":
                    expedition_btn_equip.Text = Constants.expeditionBtnSwapWeaponText;
                    if (isInBattle)
                    {
                        if (!ExpeditionManager.BattleManager.wasWeaponUnequippedThisTurn &&
                            !ExpeditionManager.BattleManager.wasWeaponEquippedThisTurn) expedition_btn_equip.Enabled = true;
                    }
                    else expedition_btn_equip.Enabled = true;
                    return;
                default:
                    throw new Exception("[Form_Expedition.UpdateEquipButtonState] Как ты это сделал???");
            }
        }

        private void expedition_listbox_accs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (expedition_listbox_accs.SelectedIndex < 0)
            {
                expedition_btn_useacc.Text = Constants.unavailableText;
                expedition_btn_useacc.Enabled = false;
                return;
            }

            Player player = ExpeditionManager.GameInstance.Player;
            UsableAccessory? selectedAcc = player.EquippedAccessories[expedition_listbox_accs.SelectedIndex] as UsableAccessory;

            expedition_btn_useacc.Enabled = false;
            // Если у аксессуара нет способности
            if (selectedAcc is null)
            {
                expedition_btn_useacc.Text = Constants.unavailableText;
                return;
            }
            // Если же есть
            else
            {
                if (selectedAcc.Ability.IsOnCooldown())
                {
                    expedition_btn_useacc.Text = $"Кулдаун: {selectedAcc.Ability.AvailableIn}";
                    return;
                }
                else
                {
                    if (!selectedAcc.Ability.UsableOutOfBattle && ExpeditionManager.BattleManager is null)
                    {
                        expedition_btn_useacc.Text = Constants.expeditionBtnUseAccOutOfBattleText;
                        return;
                    }
                    expedition_btn_useacc.Enabled = true;
                    expedition_btn_useacc.Text = Constants.expeditionBtnUseAccText;
                    return;
                }
            }
        }

        private void expedition_btn_useacc_Click(object sender, EventArgs e)
        {
            if (expedition_listbox_accs.SelectedIndex < 0) return;

            Player player = ExpeditionManager.GameInstance.Player;
            UsableAccessory? selectedAcc = player.EquippedAccessories[expedition_listbox_accs.SelectedIndex] as UsableAccessory;
            if (selectedAcc is null)
                throw new Exception("Кнопка активации доступна для аксессуара, не являющегося UsableAccessory");

            selectedAcc.UseAbility(ExpeditionManager);
            UpdateAll();
        }

        private void Form_Expedition_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        #region InfoOnHover
        private void expedition_listbox_accs_MouseHover(object sender, EventArgs e)
        {
            Point point = expedition_listbox_accs.PointToClient(Cursor.Position);
            int index = expedition_listbox_accs.IndexFromPoint(point);
            if (index < 0) return;

            var player = ExpeditionManager.GameInstance.Player;
            var item = player.EquippedAccessories[index];

            toolTip.SetToolTip(expedition_listbox_accs, $"{item.GetItemStatsAsString()}");
        }

        private void expedition_listbox_accs_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(expedition_listbox_accs);
        }

        private void expedition_listbox_weapons_MouseHover(object sender, EventArgs e)
        {
            Point point = expedition_listbox_weapons.PointToClient(Cursor.Position);
            int index = expedition_listbox_weapons.IndexFromPoint(point);
            if (index < 0) return;

            var player = ExpeditionManager.GameInstance.Player;
            var item = player.EquippedWeapons[index];

            toolTip.SetToolTip(expedition_listbox_weapons, $"{item.GetItemStatsAsString()}");
        }

        private void expedition_listbox_weapons_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(expedition_listbox_weapons);
        }

        private void expedition_listbox_effects_MouseHover(object sender, EventArgs e)
        {
            Point point = expedition_listbox_effects.PointToClient(Cursor.Position);
            int index = expedition_listbox_effects.IndexFromPoint(point);
            if (index < 0) return;

            var player = ExpeditionManager.GameInstance.Player;
            var effect = player.BattleStats.CurrentEffects[index];

            StringBuilder sb = new StringBuilder();
            sb.Append($"{effect.Name}\n\n");
            sb.Append($"{effect.Description}\n\n");
            if (effect as StackableEffect is not null)
                sb.Append($"Сила эффекта: {effect.Power}\n");
            sb.Append($"Будет действовать еще {effect.TurnsLeft} ходов");
            toolTip.SetToolTip(expedition_listbox_effects, $"{sb}");
        }

        private void expedition_listbox_effects_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(expedition_listbox_effects);
        }

        private void expedition_listbox_enemyeffects_MouseHover(object sender, EventArgs e)
        {
            Point point = expedition_listbox_enemyeffects.PointToClient(Cursor.Position);
            int index = expedition_listbox_enemyeffects.IndexFromPoint(point);
            if (index < 0) return;

            var enemy = ExpeditionManager.BattleManager.Enemy;
            var effect = enemy.BattleStats.CurrentEffects[index];

            StringBuilder sb = new StringBuilder();
            sb.Append($"{effect.Name}\n\n");
            sb.Append($"{effect.Description}\n\n");
            if (effect as StackableEffect is not null)
                sb.Append($"Сила эффекта: {effect.Power}\n");
            sb.Append($"Будет действовать еще {effect.TurnsLeft} ходов");
            toolTip.SetToolTip(expedition_listbox_enemyeffects, $"{sb}");
        }

        private void expedition_listbox_enemyeffects_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(expedition_listbox_enemyeffects);
        }
        #endregion

        // Сброс выделения всех ListBox айтемов, не находящися под мышкой
        private void Form_Expedition_MouseUp(object sender, MouseEventArgs e)
        {
            //Point point = PointToClient(Cursor.Position);
        }

        private void expedition_btn_flee_Click(object sender, EventArgs e)
        {
            var action = new Action(ActionType.FleeAttempt, 1);
            ExpeditionManager.BattleManager.MakePlayerTurn(action);
        }
    }
}
