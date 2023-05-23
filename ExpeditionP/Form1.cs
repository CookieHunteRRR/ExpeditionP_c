using ExpeditionP.GameLogic;

namespace ExpeditionP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            menu_lbl_gameversion.Text = Program.gameVersion;
        }

        private void menu_btn_startgame_Click(object sender, EventArgs e)
        {
            Program.Game.CreateGameInstance();
            this.Hide();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.L))
            {
                Program.Log.LoadLog();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}