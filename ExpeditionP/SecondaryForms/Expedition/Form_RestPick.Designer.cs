namespace ExpeditionP.SecondaryForms.Expedition
{
    partial class Form_RestPick
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
            this.restpick_lbl_chooseaction = new System.Windows.Forms.Label();
            this.restpick_btn_skip = new System.Windows.Forms.Button();
            this.restpick_btn_choose = new System.Windows.Forms.Button();
            this.restpick_listbox_actionlist = new System.Windows.Forms.ListBox();
            this.restpick_txtbox_actioninfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // restpick_lbl_chooseaction
            // 
            this.restpick_lbl_chooseaction.AutoSize = true;
            this.restpick_lbl_chooseaction.Location = new System.Drawing.Point(147, 9);
            this.restpick_lbl_chooseaction.Name = "restpick_lbl_chooseaction";
            this.restpick_lbl_chooseaction.Size = new System.Drawing.Size(113, 15);
            this.restpick_lbl_chooseaction.TabIndex = 0;
            this.restpick_lbl_chooseaction.Text = "Выберите действие";
            // 
            // restpick_btn_skip
            // 
            this.restpick_btn_skip.Location = new System.Drawing.Point(208, 197);
            this.restpick_btn_skip.Name = "restpick_btn_skip";
            this.restpick_btn_skip.Size = new System.Drawing.Size(190, 51);
            this.restpick_btn_skip.TabIndex = 8;
            this.restpick_btn_skip.Text = "skip";
            this.restpick_btn_skip.UseVisualStyleBackColor = true;
            this.restpick_btn_skip.Click += new System.EventHandler(this.restpick_btn_skip_Click);
            // 
            // restpick_btn_choose
            // 
            this.restpick_btn_choose.Enabled = false;
            this.restpick_btn_choose.Location = new System.Drawing.Point(12, 197);
            this.restpick_btn_choose.Name = "restpick_btn_choose";
            this.restpick_btn_choose.Size = new System.Drawing.Size(190, 51);
            this.restpick_btn_choose.TabIndex = 7;
            this.restpick_btn_choose.Text = "choose";
            this.restpick_btn_choose.UseVisualStyleBackColor = true;
            this.restpick_btn_choose.Click += new System.EventHandler(this.restpick_btn_choose_Click);
            // 
            // restpick_listbox_actionlist
            // 
            this.restpick_listbox_actionlist.FormattingEnabled = true;
            this.restpick_listbox_actionlist.ItemHeight = 15;
            this.restpick_listbox_actionlist.Location = new System.Drawing.Point(12, 37);
            this.restpick_listbox_actionlist.Name = "restpick_listbox_actionlist";
            this.restpick_listbox_actionlist.Size = new System.Drawing.Size(154, 154);
            this.restpick_listbox_actionlist.TabIndex = 5;
            this.restpick_listbox_actionlist.SelectedIndexChanged += new System.EventHandler(this.restpick_listbox_actionlist_SelectedIndexChanged);
            // 
            // restpick_txtbox_actioninfo
            // 
            this.restpick_txtbox_actioninfo.Location = new System.Drawing.Point(172, 37);
            this.restpick_txtbox_actioninfo.Multiline = true;
            this.restpick_txtbox_actioninfo.Name = "restpick_txtbox_actioninfo";
            this.restpick_txtbox_actioninfo.ReadOnly = true;
            this.restpick_txtbox_actioninfo.Size = new System.Drawing.Size(226, 154);
            this.restpick_txtbox_actioninfo.TabIndex = 9;
            // 
            // Form_RestPick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 258);
            this.Controls.Add(this.restpick_txtbox_actioninfo);
            this.Controls.Add(this.restpick_btn_skip);
            this.Controls.Add(this.restpick_btn_choose);
            this.Controls.Add(this.restpick_listbox_actionlist);
            this.Controls.Add(this.restpick_lbl_chooseaction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form_RestPick";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_RestPick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_RestPick_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label restpick_lbl_chooseaction;
        private Button restpick_btn_skip;
        private Button restpick_btn_choose;
        private ListBox restpick_listbox_actionlist;
        private TextBox restpick_txtbox_actioninfo;
    }
}