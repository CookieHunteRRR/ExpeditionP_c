using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.BattleLogic.Effects;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    internal class Utils
    {
        internal static bool CheckProbability(double chance)
        {
            return Program.Random.NextDouble() <= chance;
        }

        internal static int Round(double value)
        {
            return (int)Math.Round(value, MidpointRounding.AwayFromZero);
        }

        internal static string GetBeautifulChanceDisplayText(int value)
        {
            return (value > 0) ? ((value > 99) ? "100%" : $"{value}%") : "0%";
        }

        internal static Color GetColor(string colorName)
        {
            switch (colorName)
            {
                case "red":
                    return Color.Red;
                case "blue":
                    return Color.Blue;
                case "green":
                    return Color.Green;
                default:
                    return Color.Black;
            }
        }
    }
}
