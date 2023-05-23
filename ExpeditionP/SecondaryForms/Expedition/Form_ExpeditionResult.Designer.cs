namespace ExpeditionP.SecondaryForms.Expedition
{
    partial class Form_ExpeditionResult
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
            this.expresult_lbl_pickitemtext = new System.Windows.Forms.Label();
            this.expresult_richtextbox_itemstats = new System.Windows.Forms.RichTextBox();
            this.expresult_listbox_itemlist = new System.Windows.Forms.ListBox();
            this.expresult_btn_choose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // expresult_lbl_pickitemtext
            // 
            this.expresult_lbl_pickitemtext.AutoSize = true;
            this.expresult_lbl_pickitemtext.Location = new System.Drawing.Point(148, 9);
            this.expresult_lbl_pickitemtext.Name = "expresult_lbl_pickitemtext";
            this.expresult_lbl_pickitemtext.Size = new System.Drawing.Size(110, 15);
            this.expresult_lbl_pickitemtext.TabIndex = 5;
            this.expresult_lbl_pickitemtext.Text = "Выберите предмет";
            // 
            // expresult_richtextbox_itemstats
            // 
            this.expresult_richtextbox_itemstats.Location = new System.Drawing.Point(172, 35);
            this.expresult_richtextbox_itemstats.Name = "expresult_richtextbox_itemstats";
            this.expresult_richtextbox_itemstats.ReadOnly = true;
            this.expresult_richtextbox_itemstats.Size = new System.Drawing.Size(226, 154);
            this.expresult_richtextbox_itemstats.TabIndex = 4;
            this.expresult_richtextbox_itemstats.Text = "";
            // 
            // expresult_listbox_itemlist
            // 
            this.expresult_listbox_itemlist.FormattingEnabled = true;
            this.expresult_listbox_itemlist.ItemHeight = 15;
            this.expresult_listbox_itemlist.Location = new System.Drawing.Point(12, 35);
            this.expresult_listbox_itemlist.Name = "expresult_listbox_itemlist";
            this.expresult_listbox_itemlist.Size = new System.Drawing.Size(154, 154);
            this.expresult_listbox_itemlist.TabIndex = 3;
            this.expresult_listbox_itemlist.SelectedIndexChanged += new System.EventHandler(this.expresult_listbox_itemlist_SelectedIndexChanged);
            // 
            // expresult_btn_choose
            // 
            this.expresult_btn_choose.Enabled = false;
            this.expresult_btn_choose.Location = new System.Drawing.Point(12, 195);
            this.expresult_btn_choose.Name = "expresult_btn_choose";
            this.expresult_btn_choose.Size = new System.Drawing.Size(190, 51);
            this.expresult_btn_choose.TabIndex = 6;
            this.expresult_btn_choose.Text = "choose";
            this.expresult_btn_choose.UseVisualStyleBackColor = true;
            this.expresult_btn_choose.Click += new System.EventHandler(this.expresult_btn_choose_Click);
            // 
            // Form_ExpeditionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 258);
            this.Controls.Add(this.expresult_btn_choose);
            this.Controls.Add(this.expresult_lbl_pickitemtext);
            this.Controls.Add(this.expresult_richtextbox_itemstats);
            this.Controls.Add(this.expresult_listbox_itemlist);
            this.Name = "Form_ExpeditionResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_ExpeditionResult";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ExpeditionResult_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label expresult_lbl_pickitemtext;
        private RichTextBox expresult_richtextbox_itemstats;
        private ListBox expresult_listbox_itemlist;
        private Button expresult_btn_choose;
    }
}