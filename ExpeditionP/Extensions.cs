using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP
{
    public static class Extensions
    {
        public static void AddString(this RichTextBox rtb, string toAdd)
        {
            rtb.AppendText(toAdd + "\r\n");
            rtb.SelectionStart = rtb.Text.Length;
            rtb.ScrollToCaret();
        }

        public static bool IsInsideRectangle(this Point point, int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
        {
            bool isInRangeOfX = (topLeftX < point.X) && (point.X > bottomRightX);
            bool isInRangeOfY = (topLeftY > point.Y) && (point.Y < bottomRightY);
            return isInRangeOfX && isInRangeOfY;
        }
    }
}
