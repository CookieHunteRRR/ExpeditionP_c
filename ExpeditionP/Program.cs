using ExpeditionP.GameLogic;
using ExpeditionP.GameLogic.Managers;
using System.Reflection;

namespace ExpeditionP
{
    public static class Program
    {
        internal static string gameVersion = "Alpha 1.0";

        internal static Random Random = new Random();
        internal static Form1 Menu = new Form1();
        internal static Form_Hideout Hideout = new Form_Hideout();
        internal static Form_Expedition Expedition = new Form_Expedition();
        internal static Form_Log Log = new Form_Log();
        internal static Game Game;


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Game = new Game();

            Game.LoadGameContent();

            Application.Run(Menu);
        }

        static void KillProcess()
        {
            Application.Exit();
        }

        internal static void SendToLog(string line) { Log.AddLine(line); }
    }
}