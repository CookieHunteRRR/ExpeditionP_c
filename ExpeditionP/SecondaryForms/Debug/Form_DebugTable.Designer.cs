namespace ExpeditionP.SecondaryForms.Debug
{
    partial class Form_DebugTable
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
            this.debugtable_datagridview_tagtable = new System.Windows.Forms.DataGridView();
            this.debugtable_btn_itemtable = new System.Windows.Forms.Button();
            this.debugtable_btn_mobtable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.debugtable_datagridview_tagtable)).BeginInit();
            this.SuspendLayout();
            // 
            // debugtable_datagridview_tagtable
            // 
            this.debugtable_datagridview_tagtable.AllowUserToAddRows = false;
            this.debugtable_datagridview_tagtable.AllowUserToDeleteRows = false;
            this.debugtable_datagridview_tagtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.debugtable_datagridview_tagtable.Location = new System.Drawing.Point(12, 12);
            this.debugtable_datagridview_tagtable.Name = "debugtable_datagridview_tagtable";
            this.debugtable_datagridview_tagtable.ReadOnly = true;
            this.debugtable_datagridview_tagtable.RowTemplate.Height = 25;
            this.debugtable_datagridview_tagtable.Size = new System.Drawing.Size(1122, 563);
            this.debugtable_datagridview_tagtable.TabIndex = 0;
            // 
            // debugtable_btn_itemtable
            // 
            this.debugtable_btn_itemtable.Location = new System.Drawing.Point(12, 581);
            this.debugtable_btn_itemtable.Name = "debugtable_btn_itemtable";
            this.debugtable_btn_itemtable.Size = new System.Drawing.Size(99, 38);
            this.debugtable_btn_itemtable.TabIndex = 1;
            this.debugtable_btn_itemtable.Text = "Item";
            this.debugtable_btn_itemtable.UseVisualStyleBackColor = true;
            this.debugtable_btn_itemtable.Click += new System.EventHandler(this.debugtable_btn_itemtable_Click);
            // 
            // debugtable_btn_mobtable
            // 
            this.debugtable_btn_mobtable.Location = new System.Drawing.Point(117, 581);
            this.debugtable_btn_mobtable.Name = "debugtable_btn_mobtable";
            this.debugtable_btn_mobtable.Size = new System.Drawing.Size(99, 38);
            this.debugtable_btn_mobtable.TabIndex = 2;
            this.debugtable_btn_mobtable.Text = "Mob";
            this.debugtable_btn_mobtable.UseVisualStyleBackColor = true;
            this.debugtable_btn_mobtable.Click += new System.EventHandler(this.debugtable_btn_mobtable_Click);
            // 
            // Form_DebugTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 631);
            this.Controls.Add(this.debugtable_btn_mobtable);
            this.Controls.Add(this.debugtable_btn_itemtable);
            this.Controls.Add(this.debugtable_datagridview_tagtable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_DebugTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_TagTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_DebugTable_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.debugtable_datagridview_tagtable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView debugtable_datagridview_tagtable;
        private Button debugtable_btn_itemtable;
        private Button debugtable_btn_mobtable;
    }
}