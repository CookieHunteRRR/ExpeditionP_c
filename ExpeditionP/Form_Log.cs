using ExpeditionP.SecondaryForms.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpeditionP
{
    public partial class Form_Log : Form
    {
        public Form_Log()
        {
            InitializeComponent();
        }

        public void LoadLog()
        {
            this.Show();
        }

        public void AddLine(string line)
        {
            log_textbox_log.Text = log_textbox_log.Text.Insert(0, line + "\r\n");
            log_textbox_log.Update();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.T))
            {
                new Form_DebugTable().Show();
                return true;
            }
            if (keyData == (Keys.Control | Keys.D))
            {
                new Form_DamageDebug().Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form_Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
