namespace ExpeditionP
{
    partial class Form_Expedition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.expedition_pnl_user = new System.Windows.Forms.Panel();
            this.expedition_listbox_effects = new System.Windows.Forms.ListBox();
            this.expedition_pnl_weapons = new System.Windows.Forms.Panel();
            this.expedition_btn_equip = new System.Windows.Forms.Button();
            this.expedition_listbox_weapons = new System.Windows.Forms.ListBox();
            this.expedition_pnl_accs = new System.Windows.Forms.Panel();
            this.expedition_btn_useacc = new System.Windows.Forms.Button();
            this.expedition_listbox_accs = new System.Windows.Forms.ListBox();
            this.expedition_richtextbox_userstats = new System.Windows.Forms.RichTextBox();
            this.expedition_richtextbox_log = new System.Windows.Forms.RichTextBox();
            this.expedition_btn_move = new System.Windows.Forms.Button();
            this.expedition_btn_interact = new System.Windows.Forms.Button();
            this.expedition_pnl_battle = new System.Windows.Forms.Panel();
            this.expedition_btn_flee = new System.Windows.Forms.Button();
            this.expedition_btn_attack = new System.Windows.Forms.Button();
            this.expedition_listbox_enemyeffects = new System.Windows.Forms.ListBox();
            this.expedition_richtextbox_enemystats = new System.Windows.Forms.RichTextBox();
            this.expedition_pnl_user.SuspendLayout();
            this.expedition_pnl_weapons.SuspendLayout();
            this.expedition_pnl_accs.SuspendLayout();
            this.expedition_pnl_battle.SuspendLayout();
            this.SuspendLayout();
            // 
            // expedition_pnl_user
            // 
            this.expedition_pnl_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expedition_pnl_user.Controls.Add(this.expedition_listbox_effects);
            this.expedition_pnl_user.Controls.Add(this.expedition_pnl_weapons);
            this.expedition_pnl_user.Controls.Add(this.expedition_pnl_accs);
            this.expedition_pnl_user.Controls.Add(this.expedition_richtextbox_userstats);
            this.expedition_pnl_user.Location = new System.Drawing.Point(572, 12);
            this.expedition_pnl_user.Name = "expedition_pnl_user";
            this.expedition_pnl_user.Size = new System.Drawing.Size(200, 537);
            this.expedition_pnl_user.TabIndex = 1;
            // 
            // expedition_listbox_effects
            // 
            this.expedition_listbox_effects.FormattingEnabled = true;
            this.expedition_listbox_effects.ItemHeight = 15;
            this.expedition_listbox_effects.Location = new System.Drawing.Point(3, 147);
            this.expedition_listbox_effects.Name = "expedition_listbox_effects";
            this.expedition_listbox_effects.Size = new System.Drawing.Size(192, 139);
            this.expedition_listbox_effects.TabIndex = 6;
            this.expedition_listbox_effects.MouseLeave += new System.EventHandler(this.expedition_listbox_effects_MouseLeave);
            this.expedition_listbox_effects.MouseHover += new System.EventHandler(this.expedition_listbox_effects_MouseHover);
            // 
            // expedition_pnl_weapons
            // 
            this.expedition_pnl_weapons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expedition_pnl_weapons.Controls.Add(this.expedition_btn_equip);
            this.expedition_pnl_weapons.Controls.Add(this.expedition_listbox_weapons);
            this.expedition_pnl_weapons.Location = new System.Drawing.Point(3, 418);
            this.expedition_pnl_weapons.Name = "expedition_pnl_weapons";
            this.expedition_pnl_weapons.Size = new System.Drawing.Size(194, 112);
            this.expedition_pnl_weapons.TabIndex = 4;
            // 
            // expedition_btn_equip
            // 
            this.expedition_btn_equip.Enabled = false;
            this.expedition_btn_equip.Location = new System.Drawing.Point(3, 73);
            this.expedition_btn_equip.Name = "expedition_btn_equip";
            this.expedition_btn_equip.Size = new System.Drawing.Size(186, 35);
            this.expedition_btn_equip.TabIndex = 1;
            this.expedition_btn_equip.Text = "equip";
            this.expedition_btn_equip.UseVisualStyleBackColor = true;
            this.expedition_btn_equip.Click += new System.EventHandler(this.expedition_btn_equip_Click);
            // 
            // expedition_listbox_weapons
            // 
            this.expedition_listbox_weapons.FormattingEnabled = true;
            this.expedition_listbox_weapons.ItemHeight = 15;
            this.expedition_listbox_weapons.Items.AddRange(new object[] {
            "test1",
            "test2",
            "test3",
            "test4"});
            this.expedition_listbox_weapons.Location = new System.Drawing.Point(3, 3);
            this.expedition_listbox_weapons.Name = "expedition_listbox_weapons";
            this.expedition_listbox_weapons.Size = new System.Drawing.Size(186, 64);
            this.expedition_listbox_weapons.TabIndex = 0;
            this.expedition_listbox_weapons.SelectedIndexChanged += new System.EventHandler(this.expedition_listbox_weapons_SelectedIndexChanged);
            this.expedition_listbox_weapons.MouseLeave += new System.EventHandler(this.expedition_listbox_weapons_MouseLeave);
            this.expedition_listbox_weapons.MouseHover += new System.EventHandler(this.expedition_listbox_weapons_MouseHover);
            // 
            // expedition_pnl_accs
            // 
            this.expedition_pnl_accs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expedition_pnl_accs.Controls.Add(this.expedition_btn_useacc);
            this.expedition_pnl_accs.Controls.Add(this.expedition_listbox_accs);
            this.expedition_pnl_accs.Location = new System.Drawing.Point(3, 288);
            this.expedition_pnl_accs.Name = "expedition_pnl_accs";
            this.expedition_pnl_accs.Size = new System.Drawing.Size(194, 128);
            this.expedition_pnl_accs.TabIndex = 5;
            // 
            // expedition_btn_useacc
            // 
            this.expedition_btn_useacc.Enabled = false;
            this.expedition_btn_useacc.Location = new System.Drawing.Point(3, 88);
            this.expedition_btn_useacc.Name = "expedition_btn_useacc";
            this.expedition_btn_useacc.Size = new System.Drawing.Size(186, 35);
            this.expedition_btn_useacc.TabIndex = 1;
            this.expedition_btn_useacc.Text = "use";
            this.expedition_btn_useacc.UseVisualStyleBackColor = true;
            this.expedition_btn_useacc.Click += new System.EventHandler(this.expedition_btn_useacc_Click);
            // 
            // expedition_listbox_accs
            // 
            this.expedition_listbox_accs.FormattingEnabled = true;
            this.expedition_listbox_accs.ItemHeight = 15;
            this.expedition_listbox_accs.Items.AddRange(new object[] {
            "test1",
            "test2",
            "test3",
            "test4",
            "test5"});
            this.expedition_listbox_accs.Location = new System.Drawing.Point(3, 3);
            this.expedition_listbox_accs.Name = "expedition_listbox_accs";
            this.expedition_listbox_accs.Size = new System.Drawing.Size(186, 79);
            this.expedition_listbox_accs.TabIndex = 0;
            this.expedition_listbox_accs.SelectedIndexChanged += new System.EventHandler(this.expedition_listbox_accs_SelectedIndexChanged);
            this.expedition_listbox_accs.MouseLeave += new System.EventHandler(this.expedition_listbox_accs_MouseLeave);
            this.expedition_listbox_accs.MouseHover += new System.EventHandler(this.expedition_listbox_accs_MouseHover);
            // 
            // expedition_richtextbox_userstats
            // 
            this.expedition_richtextbox_userstats.Location = new System.Drawing.Point(3, 3);
            this.expedition_richtextbox_userstats.Name = "expedition_richtextbox_userstats";
            this.expedition_richtextbox_userstats.ReadOnly = true;
            this.expedition_richtextbox_userstats.Size = new System.Drawing.Size(194, 138);
            this.expedition_richtextbox_userstats.TabIndex = 0;
            this.expedition_richtextbox_userstats.Text = "";
            // 
            // expedition_richtextbox_log
            // 
            this.expedition_richtextbox_log.Location = new System.Drawing.Point(12, 12);
            this.expedition_richtextbox_log.Name = "expedition_richtextbox_log";
            this.expedition_richtextbox_log.ReadOnly = true;
            this.expedition_richtextbox_log.Size = new System.Drawing.Size(554, 282);
            this.expedition_richtextbox_log.TabIndex = 2;
            this.expedition_richtextbox_log.Text = "";
            // 
            // expedition_btn_move
            // 
            this.expedition_btn_move.Location = new System.Drawing.Point(15, 341);
            this.expedition_btn_move.Name = "expedition_btn_move";
            this.expedition_btn_move.Size = new System.Drawing.Size(150, 35);
            this.expedition_btn_move.TabIndex = 1;
            this.expedition_btn_move.Text = "move";
            this.expedition_btn_move.UseVisualStyleBackColor = true;
            this.expedition_btn_move.Click += new System.EventHandler(this.expedition_btn_move_Click);
            // 
            // expedition_btn_interact
            // 
            this.expedition_btn_interact.Enabled = false;
            this.expedition_btn_interact.Location = new System.Drawing.Point(15, 300);
            this.expedition_btn_interact.Name = "expedition_btn_interact";
            this.expedition_btn_interact.Size = new System.Drawing.Size(150, 35);
            this.expedition_btn_interact.TabIndex = 3;
            this.expedition_btn_interact.Text = "interact";
            this.expedition_btn_interact.UseVisualStyleBackColor = true;
            this.expedition_btn_interact.Click += new System.EventHandler(this.expedition_btn_interact_Click);
            // 
            // expedition_pnl_battle
            // 
            this.expedition_pnl_battle.Controls.Add(this.expedition_btn_flee);
            this.expedition_pnl_battle.Controls.Add(this.expedition_btn_attack);
            this.expedition_pnl_battle.Controls.Add(this.expedition_listbox_enemyeffects);
            this.expedition_pnl_battle.Controls.Add(this.expedition_richtextbox_enemystats);
            this.expedition_pnl_battle.Location = new System.Drawing.Point(168, 300);
            this.expedition_pnl_battle.Name = "expedition_pnl_battle";
            this.expedition_pnl_battle.Size = new System.Drawing.Size(398, 249);
            this.expedition_pnl_battle.TabIndex = 4;
            this.expedition_pnl_battle.Visible = false;
            // 
            // expedition_btn_flee
            // 
            this.expedition_btn_flee.Location = new System.Drawing.Point(203, 5);
            this.expedition_btn_flee.Name = "expedition_btn_flee";
            this.expedition_btn_flee.Size = new System.Drawing.Size(192, 38);
            this.expedition_btn_flee.TabIndex = 3;
            this.expedition_btn_flee.Text = "flee";
            this.expedition_btn_flee.UseVisualStyleBackColor = true;
            this.expedition_btn_flee.Click += new System.EventHandler(this.expedition_btn_flee_Click);
            // 
            // expedition_btn_attack
            // 
            this.expedition_btn_attack.Location = new System.Drawing.Point(203, 202);
            this.expedition_btn_attack.Name = "expedition_btn_attack";
            this.expedition_btn_attack.Size = new System.Drawing.Size(192, 38);
            this.expedition_btn_attack.TabIndex = 2;
            this.expedition_btn_attack.Text = "atk";
            this.expedition_btn_attack.UseVisualStyleBackColor = true;
            this.expedition_btn_attack.Click += new System.EventHandler(this.expedition_btn_attack_Click);
            // 
            // expedition_listbox_enemyeffects
            // 
            this.expedition_listbox_enemyeffects.FormattingEnabled = true;
            this.expedition_listbox_enemyeffects.ItemHeight = 15;
            this.expedition_listbox_enemyeffects.Location = new System.Drawing.Point(3, 146);
            this.expedition_listbox_enemyeffects.Name = "expedition_listbox_enemyeffects";
            this.expedition_listbox_enemyeffects.Size = new System.Drawing.Size(194, 94);
            this.expedition_listbox_enemyeffects.TabIndex = 1;
            this.expedition_listbox_enemyeffects.MouseLeave += new System.EventHandler(this.expedition_listbox_enemyeffects_MouseLeave);
            this.expedition_listbox_enemyeffects.MouseHover += new System.EventHandler(this.expedition_listbox_enemyeffects_MouseHover);
            // 
            // expedition_richtextbox_enemystats
            // 
            this.expedition_richtextbox_enemystats.Location = new System.Drawing.Point(3, 3);
            this.expedition_richtextbox_enemystats.Name = "expedition_richtextbox_enemystats";
            this.expedition_richtextbox_enemystats.ReadOnly = true;
            this.expedition_richtextbox_enemystats.Size = new System.Drawing.Size(194, 138);
            this.expedition_richtextbox_enemystats.TabIndex = 0;
            this.expedition_richtextbox_enemystats.Text = "";
            // 
            // Form_Expedition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.expedition_pnl_battle);
            this.Controls.Add(this.expedition_btn_interact);
            this.Controls.Add(this.expedition_btn_move);
            this.Controls.Add(this.expedition_richtextbox_log);
            this.Controls.Add(this.expedition_pnl_user);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Expedition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Expedition";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Expedition_FormClosed);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_Expedition_MouseUp);
            this.expedition_pnl_user.ResumeLayout(false);
            this.expedition_pnl_weapons.ResumeLayout(false);
            this.expedition_pnl_accs.ResumeLayout(false);
            this.expedition_pnl_battle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel expedition_pnl_user;
        private RichTextBox expedition_richtextbox_userstats;
        private RichTextBox expedition_richtextbox_log;
        private Button expedition_btn_move;
        private Button expedition_btn_interact;
        private Panel expedition_pnl_weapons;
        private ListBox expedition_listbox_weapons;
        private Button expedition_btn_equip;
        private Panel expedition_pnl_accs;
        private Button expedition_btn_useacc;
        private ListBox expedition_listbox_accs;
        private ListBox expedition_listbox_effects;
        private Panel expedition_pnl_battle;
        private RichTextBox expedition_richtextbox_enemystats;
        private ListBox expedition_listbox_enemyeffects;
        private Button expedition_btn_attack;
        private Button expedition_btn_flee;
    }
}