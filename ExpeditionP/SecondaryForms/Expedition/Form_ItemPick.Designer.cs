namespace ExpeditionP.SecondaryForms.Expedition
{
    partial class Form_ItemPick
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
            this.itempick_listbox_itemlist = new System.Windows.Forms.ListBox();
            this.itempick_richtextbox_itemstats = new System.Windows.Forms.RichTextBox();
            this.itempick_lbl_pickitemtext = new System.Windows.Forms.Label();
            this.itempick_btn_choose = new System.Windows.Forms.Button();
            this.itempick_btn_skip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // itempick_listbox_itemlist
            // 
            this.itempick_listbox_itemlist.FormattingEnabled = true;
            this.itempick_listbox_itemlist.ItemHeight = 15;
            this.itempick_listbox_itemlist.Location = new System.Drawing.Point(12, 35);
            this.itempick_listbox_itemlist.Name = "itempick_listbox_itemlist";
            this.itempick_listbox_itemlist.Size = new System.Drawing.Size(154, 154);
            this.itempick_listbox_itemlist.TabIndex = 0;
            this.itempick_listbox_itemlist.SelectedIndexChanged += new System.EventHandler(this.itempick_listbox_itemlist_SelectedIndexChanged);
            // 
            // itempick_richtextbox_itemstats
            // 
            this.itempick_richtextbox_itemstats.Location = new System.Drawing.Point(172, 35);
            this.itempick_richtextbox_itemstats.Name = "itempick_richtextbox_itemstats";
            this.itempick_richtextbox_itemstats.ReadOnly = true;
            this.itempick_richtextbox_itemstats.Size = new System.Drawing.Size(226, 154);
            this.itempick_richtextbox_itemstats.TabIndex = 1;
            this.itempick_richtextbox_itemstats.Text = "";
            // 
            // itempick_lbl_pickitemtext
            // 
            this.itempick_lbl_pickitemtext.AutoSize = true;
            this.itempick_lbl_pickitemtext.Location = new System.Drawing.Point(148, 9);
            this.itempick_lbl_pickitemtext.Name = "itempick_lbl_pickitemtext";
            this.itempick_lbl_pickitemtext.Size = new System.Drawing.Size(110, 15);
            this.itempick_lbl_pickitemtext.TabIndex = 2;
            this.itempick_lbl_pickitemtext.Text = "Выберите предмет";
            // 
            // itempick_btn_choose
            // 
            this.itempick_btn_choose.Enabled = false;
            this.itempick_btn_choose.Location = new System.Drawing.Point(12, 195);
            this.itempick_btn_choose.Name = "itempick_btn_choose";
            this.itempick_btn_choose.Size = new System.Drawing.Size(190, 51);
            this.itempick_btn_choose.TabIndex = 3;
            this.itempick_btn_choose.Text = "choose";
            this.itempick_btn_choose.UseVisualStyleBackColor = true;
            this.itempick_btn_choose.Click += new System.EventHandler(this.itempick_btn_choose_Click);
            // 
            // itempick_btn_skip
            // 
            this.itempick_btn_skip.Location = new System.Drawing.Point(208, 195);
            this.itempick_btn_skip.Name = "itempick_btn_skip";
            this.itempick_btn_skip.Size = new System.Drawing.Size(190, 51);
            this.itempick_btn_skip.TabIndex = 4;
            this.itempick_btn_skip.Text = "skip";
            this.itempick_btn_skip.UseVisualStyleBackColor = true;
            this.itempick_btn_skip.Click += new System.EventHandler(this.itempick_btn_skip_Click);
            // 
            // Form_ItemPick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 258);
            this.Controls.Add(this.itempick_btn_skip);
            this.Controls.Add(this.itempick_btn_choose);
            this.Controls.Add(this.itempick_lbl_pickitemtext);
            this.Controls.Add(this.itempick_richtextbox_itemstats);
            this.Controls.Add(this.itempick_listbox_itemlist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_ItemPick";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_ItemPick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ItemPick_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox itempick_listbox_itemlist;
        private RichTextBox itempick_richtextbox_itemstats;
        private Label itempick_lbl_pickitemtext;
        private Button itempick_btn_choose;
        private Button itempick_btn_skip;
    }
}