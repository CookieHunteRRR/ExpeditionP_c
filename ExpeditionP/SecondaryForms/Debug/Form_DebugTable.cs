using ExpeditionP.GameLogic.Holders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpeditionP.SecondaryForms.Debug
{
    public partial class Form_DebugTable : Form
    {
        public Form_DebugTable()
        {
            InitializeComponent();
        }

        void LoadItemTable()
        {
            DataTable table = ItemHolder.ItemTable;
            debugtable_datagridview_tagtable.DataSource = table;
        }

        void LoadEntityTable()
        {
            DataTable table = EntityHolder.EntityTable;
            debugtable_datagridview_tagtable.DataSource = table;
        }

        private void Form_DebugTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void debugtable_btn_itemtable_Click(object sender, EventArgs e)
        {
            LoadItemTable();
        }

        private void debugtable_btn_mobtable_Click(object sender, EventArgs e)
        {
            LoadEntityTable();
        }
    }
}
