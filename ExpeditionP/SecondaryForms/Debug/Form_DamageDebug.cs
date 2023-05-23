using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
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
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.SecondaryForms.Debug
{
    /*
     * вводим статы оружия (или айди, статы сами спарсятся)
        вводим остальные статы или добавляем шмотки по айди
    вводим статы моба либо его айди
     */
    public partial class Form_DamageDebug : Form
    {
        Player DebugPlayer { get; set; }
        Mob DebugMob { get; set; }
        List<AttackInstance> PlayerAtkInstances { get; set; }
        List<AttackInstance> EnemyAtkInstances { get; set; }

        int playerCritAttackCount = 0;
        int playerMissAttackCount = 0;
        int playerTotalDamage = 0;
        int enemyCritAttackCount = 0;
        int enemyMissAttackCount = 0;
        int enemyTotalDamage = 0;

        public Form_DamageDebug()
        {
            InitializeComponent();
            LoadForm();
        }

        void LoadForm()
        {
            // Создаем чистого игрока и моба
            DebugPlayer = new Player();
            DebugPlayer.RecalculateStats();
            DebugMob = ((Mob)EntityHolder.RegisteredEntities["mob_standart_zombie"]).Copy();
            PlayerAtkInstances = new List<AttackInstance>();
            EnemyAtkInstances = new List<AttackInstance>();

            ResetInstances();
            UpdateUI();
        }

        void ResetInstances()
        {
            PlayerAtkInstances.Clear();
            EnemyAtkInstances.Clear();
            playerCritAttackCount = 0;
            playerMissAttackCount = 0;
            playerTotalDamage = 0;
            enemyCritAttackCount = 0;
            enemyMissAttackCount = 0;
            enemyTotalDamage = 0;
        }

        void UpdateUI()
        {
            UpdatePlayerStatsUI();
            UpdateEnemyStatsUI();
        }

        void UpdatePlayerStatsUI()
        {
            dmgdbg_textbox_pcc.Text = DebugPlayer.BattleStats.CurrentEntityStats.CritChance.ToString();
            dmgdbg_textbox_pcd.Text = DebugPlayer.BattleStats.CurrentEntityStats.CritDamage.ToString();
            dmgdbg_textbox_pdef.Text = DebugPlayer.BattleStats.CurrentEntityStats.Defense.ToString();
            dmgdbg_textbox_peva.Text = DebugPlayer.BattleStats.CurrentEntityStats.Evasion.ToString();
            dmgdbg_textbox_pamp.Text = DebugPlayer.BattleStats.CurrentEntityStats.Amplification.ToString();
            dmgdbg_textbox_pmtg.Text = DebugPlayer.BattleStats.CurrentEntityStats.Mitigation.ToString();
            dmgdbg_textbox_pannul.Text = DebugPlayer.BattleStats.CurrentEntityStats.Annulment.ToString();
            dmgdbg_textbox_pmaxhp.Text = DebugPlayer.BattleStats.CurrentEntityStats.Health.ToString();
            dmgdbg_textbox_pcurrhp.Text = DebugPlayer.BattleStats.CurrentHealth.ToString();
            dmgdbg_textbox_pmaxmana.Text = DebugPlayer.BattleStats.CurrentEntityStats.Mana.ToString();
            dmgdbg_textbox_pcurrmana.Text = DebugPlayer.BattleStats.CurrentMana.ToString();
            dmgdbg_textbox_pattackclass.Text = DebugPlayer.CurrentEquippedWeapon.Attack.GetType().Name;
            dmgdbg_textbox_pmindamage.Text = DebugPlayer.CurrentEquippedWeapon.Attack.MinDamage.ToString();
            dmgdbg_textbox_pmaxdamage.Text = DebugPlayer.CurrentEquippedWeapon.Attack.MaxDamage.ToString();
            dmgdbg_textbox_pdmgtype.Text = DebugPlayer.CurrentEquippedWeapon.Attack.DamageType.ToString();
            dmgdbg_lbl_equippedweaponname.Text = DebugPlayer.CurrentEquippedWeapon.Info.Name;
        }

        void UpdateEnemyStatsUI()
        {
            var enemyBS = DebugMob.BattleStats;
            dmgdbg_textbox_ecc.Text = enemyBS.CurrentEntityStats.CritChance.ToString();
            dmgdbg_textbox_ecd.Text = enemyBS.CurrentEntityStats.CritDamage.ToString();
            dmgdbg_textbox_edef.Text = enemyBS.CurrentEntityStats.Defense.ToString();
            dmgdbg_textbox_eeva.Text = enemyBS.CurrentEntityStats.Evasion.ToString();
            dmgdbg_textbox_eamp.Text = enemyBS.CurrentEntityStats.Amplification.ToString();
            dmgdbg_textbox_emtg.Text = enemyBS.CurrentEntityStats.Mitigation.ToString();
            dmgdbg_textbox_eannul.Text = enemyBS.CurrentEntityStats.Annulment.ToString();
            dmgdbg_textbox_emaxhp.Text = enemyBS.CurrentEntityStats.Health.ToString();
            dmgdbg_textbox_ecurrhp.Text = enemyBS.CurrentHealth.ToString();
            dmgdbg_textbox_emaxmana.Text = enemyBS.CurrentEntityStats.Mana.ToString();
            dmgdbg_textbox_ecurrmana.Text = enemyBS.CurrentMana.ToString();
            var enemyAtk = DebugMob.AI.GetAttackByIndex(0);
            dmgdbg_textbox_eattackclass.Text = enemyAtk.GetType().Name;
            dmgdbg_textbox_emindamage.Text = enemyAtk.MinDamage.ToString();
            dmgdbg_textbox_emaxdamage.Text = enemyAtk.MaxDamage.ToString();
            dmgdbg_textbox_edmgtype.Text = enemyAtk.DamageType.ToString();
            dmgdbg_lbl_enemyname.Text = DebugMob.GetName();
        }

        void CheckAndUpdateStats()
        {
            CheckAndUpdatePlayerStats();
            CheckAndUpdateEnemyStats();
        }

        void CheckAndUpdatePlayerStats()
        {
            // Для каждого стата - пробуем конвертировать введенные данные и пихнуть их в стат, если не получится пихаем дефолтный стат
            try { DebugPlayer.BattleStats.CurrentEntityStats.CritChance = Int32.Parse(dmgdbg_textbox_pcc.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.CritChance = DebugPlayer.Stats.CritChance; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.CritDamage = Int32.Parse(dmgdbg_textbox_pcd.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.CritDamage = DebugPlayer.Stats.CritDamage; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.Defense = Int32.Parse(dmgdbg_textbox_pdef.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Defense = DebugPlayer.Stats.Defense; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.Evasion = Int32.Parse(dmgdbg_textbox_peva.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Evasion = DebugPlayer.Stats.Evasion; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.Amplification = Int32.Parse(dmgdbg_textbox_pamp.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Amplification = DebugPlayer.Stats.Amplification; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.Mitigation = Int32.Parse(dmgdbg_textbox_pmtg.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Mitigation = DebugPlayer.Stats.Mitigation; }
            try { DebugPlayer.BattleStats.CurrentEntityStats.Annulment = Int32.Parse(dmgdbg_textbox_pannul.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Annulment = DebugPlayer.Stats.Annulment; }

            // короче атаки не трогают хп и ману, они нужны для скейлинга каких нибудь энмити стамин если добавлю
            try { DebugPlayer.BattleStats.CurrentEntityStats.Health = Int32.Parse(dmgdbg_textbox_pmaxhp.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Health = DebugPlayer.Stats.Health; }
            try { DebugPlayer.BattleStats.CurrentHealth = Int32.Parse(dmgdbg_textbox_pcurrhp.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentHealth = DebugPlayer.BattleStats.CurrentEntityStats.Health; }

            try { DebugPlayer.BattleStats.CurrentEntityStats.Mana = Int32.Parse(dmgdbg_textbox_pmaxmana.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentEntityStats.Mana = DebugPlayer.Stats.Mana; }
            try { DebugPlayer.BattleStats.CurrentMana = Int32.Parse(dmgdbg_textbox_pcurrmana.Text); }
            catch (Exception) { DebugPlayer.BattleStats.CurrentMana = DebugPlayer.BattleStats.CurrentEntityStats.Mana; }

            UpdatePlayerStatsUI();
        }

        void CheckAndUpdateEnemyStats()
        {
            // Для каждого стата - пробуем конвертировать введенные данные и пихнуть их в стат, если не получится пихаем дефолтный стат
            try { DebugMob.BattleStats.CurrentEntityStats.CritChance = Int32.Parse(dmgdbg_textbox_ecc.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.CritChance = DebugMob.Stats.CritChance; }
            try { DebugMob.BattleStats.CurrentEntityStats.CritDamage = Int32.Parse(dmgdbg_textbox_ecd.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.CritDamage = DebugMob.Stats.CritDamage; }
            try { DebugMob.BattleStats.CurrentEntityStats.Defense = Int32.Parse(dmgdbg_textbox_edef.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Defense = DebugMob.Stats.Defense; }
            try { DebugMob.BattleStats.CurrentEntityStats.Evasion = Int32.Parse(dmgdbg_textbox_eeva.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Evasion = DebugMob.Stats.Evasion; }
            try { DebugMob.BattleStats.CurrentEntityStats.Amplification = Int32.Parse(dmgdbg_textbox_eamp.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Amplification = DebugMob.Stats.Amplification; }
            try { DebugMob.BattleStats.CurrentEntityStats.Mitigation = Int32.Parse(dmgdbg_textbox_emtg.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Mitigation = DebugMob.Stats.Mitigation; }
            try { DebugMob.BattleStats.CurrentEntityStats.Annulment = Int32.Parse(dmgdbg_textbox_eannul.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Annulment = DebugMob.Stats.Annulment; }

            try { DebugMob.BattleStats.CurrentEntityStats.Health = Int32.Parse(dmgdbg_textbox_emaxhp.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Health = DebugMob.Stats.Health; }
            try { DebugMob.BattleStats.CurrentHealth = Int32.Parse(dmgdbg_textbox_ecurrhp.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentHealth = DebugMob.BattleStats.CurrentEntityStats.Health; }

            try { DebugMob.BattleStats.CurrentEntityStats.Mana = Int32.Parse(dmgdbg_textbox_emaxmana.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentEntityStats.Mana = DebugMob.Stats.Mana; }
            try { DebugMob.BattleStats.CurrentMana = Int32.Parse(dmgdbg_textbox_ecurrmana.Text); }
            catch (Exception) { DebugMob.BattleStats.CurrentMana = DebugMob.BattleStats.CurrentEntityStats.Mana; }

            UpdateEnemyStatsUI();
        }

        private void dmgdbg_btn_playerattack_Click(object sender, EventArgs e)
        {

        }

        private void dmgdbg_btn_enemyattack_Click(object sender, EventArgs e)
        {

        }

        private void dmgdbg_btn_setplayerweapon_Click(object sender, EventArgs e)
        {
            try
            {
                DebugPlayer.CurrentEquippedWeapon = (Weapon)ItemHolder.RegisteredItems[dmgdbg_textbox_setplayerweapon.Text];
            }
            catch (Exception) { DebugPlayer.CurrentEquippedWeapon = Constants.GetFistsWeapon(); }
            DebugPlayer.RecalculateStats();
            dmgdbg_textbox_setplayerweapon.Text = String.Empty;
            UpdatePlayerStatsUI();
        }

        private void dmgdbg_btn_setmob_Click(object sender, EventArgs e)
        {
            try
            {
                DebugMob = ((Mob)EntityHolder.RegisteredEntities[dmgdbg_textbox_setmob.Text]).Copy();
            }
            catch (Exception) { DebugMob = ((Mob)EntityHolder.RegisteredEntities["mob_standart_zombie"]).Copy(); }
            dmgdbg_textbox_setmob.Text = String.Empty;
            UpdateEnemyStatsUI();
        }

        private void dmgdbg_btn_clearattinstlist_Click(object sender, EventArgs e)
        {
            ResetInstances();
        }
    }
}
