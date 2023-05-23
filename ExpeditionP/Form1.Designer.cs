namespace ExpeditionP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu_btn_startgame = new System.Windows.Forms.Button();
            this.menu_lbl_gamename = new System.Windows.Forms.Label();
            this.menu_lbl_gameversiontext = new System.Windows.Forms.Label();
            this.menu_lbl_gameversion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menu_btn_startgame
            // 
            this.menu_btn_startgame.Location = new System.Drawing.Point(334, 238);
            this.menu_btn_startgame.Name = "menu_btn_startgame";
            this.menu_btn_startgame.Size = new System.Drawing.Size(116, 23);
            this.menu_btn_startgame.TabIndex = 0;
            this.menu_btn_startgame.Text = "НАЧИНАЕМ";
            this.menu_btn_startgame.UseVisualStyleBackColor = true;
            this.menu_btn_startgame.Click += new System.EventHandler(this.menu_btn_startgame_Click);
            // 
            // menu_lbl_gamename
            // 
            this.menu_lbl_gamename.AutoSize = true;
            this.menu_lbl_gamename.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menu_lbl_gamename.Location = new System.Drawing.Point(12, 9);
            this.menu_lbl_gamename.Name = "menu_lbl_gamename";
            this.menu_lbl_gamename.Size = new System.Drawing.Size(188, 45);
            this.menu_lbl_gamename.TabIndex = 1;
            this.menu_lbl_gamename.Text = "ExpeditionP";
            // 
            // menu_lbl_gameversiontext
            // 
            this.menu_lbl_gameversiontext.AutoSize = true;
            this.menu_lbl_gameversiontext.Location = new System.Drawing.Point(12, 65);
            this.menu_lbl_gameversiontext.Name = "menu_lbl_gameversiontext";
            this.menu_lbl_gameversiontext.Size = new System.Drawing.Size(49, 15);
            this.menu_lbl_gameversiontext.TabIndex = 2;
            this.menu_lbl_gameversiontext.Text = "Версия:";
            // 
            // menu_lbl_gameversion
            // 
            this.menu_lbl_gameversion.AutoSize = true;
            this.menu_lbl_gameversion.Location = new System.Drawing.Point(67, 65);
            this.menu_lbl_gameversion.Name = "menu_lbl_gameversion";
            this.menu_lbl_gameversion.Size = new System.Drawing.Size(55, 15);
            this.menu_lbl_gameversion.TabIndex = 3;
            this.menu_lbl_gameversion.Text = "*version*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menu_lbl_gameversion);
            this.Controls.Add(this.menu_lbl_gameversiontext);
            this.Controls.Add(this.menu_lbl_gamename);
            this.Controls.Add(this.menu_btn_startgame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button menu_btn_startgame;
        private Label menu_lbl_gamename;
        private Label menu_lbl_gameversiontext;
        private Label menu_lbl_gameversion;
    }
}