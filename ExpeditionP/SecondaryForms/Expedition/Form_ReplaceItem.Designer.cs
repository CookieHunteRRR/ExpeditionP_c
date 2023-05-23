namespace ExpeditionP.SecondaryForms.Expedition
{
    partial class Form_ReplaceItem
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
            this.replaceitem_lbl_replaceitemtext = new System.Windows.Forms.Label();
            this.replaceitem_richtextbox_newitemstats = new System.Windows.Forms.RichTextBox();
            this.replaceitem_lbl_arrowtext = new System.Windows.Forms.Label();
            this.replaceitem_richtextbox_selecteditemstats = new System.Windows.Forms.RichTextBox();
            this.replaceitem_listbox_equippeditems = new System.Windows.Forms.ListBox();
            this.replaceitem_btn_replace = new System.Windows.Forms.Button();
            this.replaceitem_btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // replaceitem_lbl_replaceitemtext
            // 
            this.replaceitem_lbl_replaceitemtext.AutoSize = true;
            this.replaceitem_lbl_replaceitemtext.Location = new System.Drawing.Point(172, 9);
            this.replaceitem_lbl_replaceitemtext.Name = "replaceitem_lbl_replaceitemtext";
            this.replaceitem_lbl_replaceitemtext.Size = new System.Drawing.Size(109, 15);
            this.replaceitem_lbl_replaceitemtext.TabIndex = 0;
            this.replaceitem_lbl_replaceitemtext.Text = "Заменить предмет";
            // 
            // replaceitem_richtextbox_newitemstats
            // 
            this.replaceitem_richtextbox_newitemstats.Location = new System.Drawing.Point(258, 30);
            this.replaceitem_richtextbox_newitemstats.Name = "replaceitem_richtextbox_newitemstats";
            this.replaceitem_richtextbox_newitemstats.Size = new System.Drawing.Size(192, 125);
            this.replaceitem_richtextbox_newitemstats.TabIndex = 1;
            this.replaceitem_richtextbox_newitemstats.Text = "";
            // 
            // replaceitem_lbl_arrowtext
            // 
            this.replaceitem_lbl_arrowtext.AutoSize = true;
            this.replaceitem_lbl_arrowtext.Location = new System.Drawing.Point(217, 82);
            this.replaceitem_lbl_arrowtext.Name = "replaceitem_lbl_arrowtext";
            this.replaceitem_lbl_arrowtext.Size = new System.Drawing.Size(28, 15);
            this.replaceitem_lbl_arrowtext.TabIndex = 2;
            this.replaceitem_lbl_arrowtext.Text = "< --";
            // 
            // replaceitem_richtextbox_selecteditemstats
            // 
            this.replaceitem_richtextbox_selecteditemstats.Location = new System.Drawing.Point(12, 30);
            this.replaceitem_richtextbox_selecteditemstats.Name = "replaceitem_richtextbox_selecteditemstats";
            this.replaceitem_richtextbox_selecteditemstats.Size = new System.Drawing.Size(192, 125);
            this.replaceitem_richtextbox_selecteditemstats.TabIndex = 3;
            this.replaceitem_richtextbox_selecteditemstats.Text = "";
            // 
            // replaceitem_listbox_equippeditems
            // 
            this.replaceitem_listbox_equippeditems.FormattingEnabled = true;
            this.replaceitem_listbox_equippeditems.ItemHeight = 15;
            this.replaceitem_listbox_equippeditems.Location = new System.Drawing.Point(12, 161);
            this.replaceitem_listbox_equippeditems.Name = "replaceitem_listbox_equippeditems";
            this.replaceitem_listbox_equippeditems.Size = new System.Drawing.Size(192, 79);
            this.replaceitem_listbox_equippeditems.TabIndex = 4;
            this.replaceitem_listbox_equippeditems.SelectedIndexChanged += new System.EventHandler(this.replaceitem_listbox_equippeditems_SelectedIndexChanged);
            // 
            // replaceitem_btn_replace
            // 
            this.replaceitem_btn_replace.Location = new System.Drawing.Point(258, 161);
            this.replaceitem_btn_replace.Name = "replaceitem_btn_replace";
            this.replaceitem_btn_replace.Size = new System.Drawing.Size(192, 36);
            this.replaceitem_btn_replace.TabIndex = 5;
            this.replaceitem_btn_replace.Text = "replace";
            this.replaceitem_btn_replace.UseVisualStyleBackColor = true;
            this.replaceitem_btn_replace.Click += new System.EventHandler(this.replaceitem_btn_replace_Click);
            // 
            // replaceitem_btn_cancel
            // 
            this.replaceitem_btn_cancel.Location = new System.Drawing.Point(258, 204);
            this.replaceitem_btn_cancel.Name = "replaceitem_btn_cancel";
            this.replaceitem_btn_cancel.Size = new System.Drawing.Size(192, 36);
            this.replaceitem_btn_cancel.TabIndex = 6;
            this.replaceitem_btn_cancel.Text = "cancel";
            this.replaceitem_btn_cancel.UseVisualStyleBackColor = true;
            this.replaceitem_btn_cancel.Click += new System.EventHandler(this.replaceitem_btn_cancel_Click);
            // 
            // Form_ReplaceItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 246);
            this.Controls.Add(this.replaceitem_btn_cancel);
            this.Controls.Add(this.replaceitem_btn_replace);
            this.Controls.Add(this.replaceitem_listbox_equippeditems);
            this.Controls.Add(this.replaceitem_richtextbox_selecteditemstats);
            this.Controls.Add(this.replaceitem_lbl_arrowtext);
            this.Controls.Add(this.replaceitem_richtextbox_newitemstats);
            this.Controls.Add(this.replaceitem_lbl_replaceitemtext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_ReplaceItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label replaceitem_lbl_replaceitemtext;
        private RichTextBox replaceitem_richtextbox_newitemstats;
        private Label replaceitem_lbl_arrowtext;
        private RichTextBox replaceitem_richtextbox_selecteditemstats;
        private ListBox replaceitem_listbox_equippeditems;
        private Button replaceitem_btn_replace;
        private Button replaceitem_btn_cancel;
    }
}