namespace ExpeditionP
{
    partial class Form_Hideout
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
            this.hideout_listbox_availablemaps = new System.Windows.Forms.ListBox();
            this.hideout_btn_launchmap = new System.Windows.Forms.Button();
            this.hideout_lbl_chooseexpedition = new System.Windows.Forms.Label();
            this.hideout_lbl_chooseitem = new System.Windows.Forms.Label();
            this.hideout_btn_selectitem = new System.Windows.Forms.Button();
            this.hideout_listbox_availableitems = new System.Windows.Forms.ListBox();
            this.hideout_lbl_pickeditemlbl = new System.Windows.Forms.Label();
            this.hideout_lbl_pickeditemname = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hideout_listbox_availablemaps
            // 
            this.hideout_listbox_availablemaps.FormattingEnabled = true;
            this.hideout_listbox_availablemaps.ItemHeight = 15;
            this.hideout_listbox_availablemaps.Location = new System.Drawing.Point(605, 30);
            this.hideout_listbox_availablemaps.Name = "hideout_listbox_availablemaps";
            this.hideout_listbox_availablemaps.Size = new System.Drawing.Size(167, 394);
            this.hideout_listbox_availablemaps.TabIndex = 0;
            // 
            // hideout_btn_launchmap
            // 
            this.hideout_btn_launchmap.Location = new System.Drawing.Point(605, 430);
            this.hideout_btn_launchmap.Name = "hideout_btn_launchmap";
            this.hideout_btn_launchmap.Size = new System.Drawing.Size(167, 23);
            this.hideout_btn_launchmap.TabIndex = 1;
            this.hideout_btn_launchmap.Text = "Отправиться";
            this.hideout_btn_launchmap.UseVisualStyleBackColor = true;
            this.hideout_btn_launchmap.Click += new System.EventHandler(this.hideout_btn_launchmap_Click);
            // 
            // hideout_lbl_chooseexpedition
            // 
            this.hideout_lbl_chooseexpedition.AutoSize = true;
            this.hideout_lbl_chooseexpedition.Location = new System.Drawing.Point(622, 9);
            this.hideout_lbl_chooseexpedition.Name = "hideout_lbl_chooseexpedition";
            this.hideout_lbl_chooseexpedition.Size = new System.Drawing.Size(132, 15);
            this.hideout_lbl_chooseexpedition.TabIndex = 2;
            this.hideout_lbl_chooseexpedition.Text = "Выберите экспедицию";
            // 
            // hideout_lbl_chooseitem
            // 
            this.hideout_lbl_chooseitem.AutoSize = true;
            this.hideout_lbl_chooseitem.Location = new System.Drawing.Point(12, 9);
            this.hideout_lbl_chooseitem.Name = "hideout_lbl_chooseitem";
            this.hideout_lbl_chooseitem.Size = new System.Drawing.Size(171, 15);
            this.hideout_lbl_chooseitem.TabIndex = 5;
            this.hideout_lbl_chooseitem.Text = "Выберите стартовый предмет";
            // 
            // hideout_btn_selectitem
            // 
            this.hideout_btn_selectitem.Location = new System.Drawing.Point(12, 430);
            this.hideout_btn_selectitem.Name = "hideout_btn_selectitem";
            this.hideout_btn_selectitem.Size = new System.Drawing.Size(167, 23);
            this.hideout_btn_selectitem.TabIndex = 4;
            this.hideout_btn_selectitem.Text = "Выбрать";
            this.hideout_btn_selectitem.UseVisualStyleBackColor = true;
            this.hideout_btn_selectitem.Click += new System.EventHandler(this.hideout_btn_selectitem_Click);
            // 
            // hideout_listbox_availableitems
            // 
            this.hideout_listbox_availableitems.FormattingEnabled = true;
            this.hideout_listbox_availableitems.ItemHeight = 15;
            this.hideout_listbox_availableitems.Location = new System.Drawing.Point(12, 30);
            this.hideout_listbox_availableitems.Name = "hideout_listbox_availableitems";
            this.hideout_listbox_availableitems.Size = new System.Drawing.Size(167, 394);
            this.hideout_listbox_availableitems.TabIndex = 3;
            // 
            // hideout_lbl_pickeditemlbl
            // 
            this.hideout_lbl_pickeditemlbl.AutoSize = true;
            this.hideout_lbl_pickeditemlbl.Location = new System.Drawing.Point(12, 456);
            this.hideout_lbl_pickeditemlbl.Name = "hideout_lbl_pickeditemlbl";
            this.hideout_lbl_pickeditemlbl.Size = new System.Drawing.Size(53, 15);
            this.hideout_lbl_pickeditemlbl.TabIndex = 6;
            this.hideout_lbl_pickeditemlbl.Text = "Выбран:";
            // 
            // hideout_lbl_pickeditemname
            // 
            this.hideout_lbl_pickeditemname.AutoSize = true;
            this.hideout_lbl_pickeditemname.Location = new System.Drawing.Point(12, 471);
            this.hideout_lbl_pickeditemname.Name = "hideout_lbl_pickeditemname";
            this.hideout_lbl_pickeditemname.Size = new System.Drawing.Size(12, 15);
            this.hideout_lbl_pickeditemname.TabIndex = 7;
            this.hideout_lbl_pickeditemname.Text = "-";
            // 
            // Form_Hideout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.hideout_lbl_pickeditemname);
            this.Controls.Add(this.hideout_lbl_pickeditemlbl);
            this.Controls.Add(this.hideout_lbl_chooseitem);
            this.Controls.Add(this.hideout_btn_selectitem);
            this.Controls.Add(this.hideout_listbox_availableitems);
            this.Controls.Add(this.hideout_lbl_chooseexpedition);
            this.Controls.Add(this.hideout_btn_launchmap);
            this.Controls.Add(this.hideout_listbox_availablemaps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Hideout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Hideout";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Hideout_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox hideout_listbox_availablemaps;
        private Button hideout_btn_launchmap;
        private Label hideout_lbl_chooseexpedition;
        private Label hideout_lbl_chooseitem;
        private Button hideout_btn_selectitem;
        private ListBox hideout_listbox_availableitems;
        private Label hideout_lbl_pickeditemlbl;
        private Label hideout_lbl_pickeditemname;
    }
}