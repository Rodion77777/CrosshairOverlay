using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrosshairOverlay
{
    internal class Limits
    {
        public static double screenWidth = SystemParameters.PrimaryScreenWidth;
        public static double screenHeight = SystemParameters.PrimaryScreenHeight;
        // Common
        public static int radius = Math.Min((int)screenWidth, (int)screenHeight) / 2;
        public static int minTickness = 0;
        public static double minOpacity = 0.1;
        public static double maxOpacity = 1;
        // Ellips A
        public static int tickness = 5;
        // Ellips B
        public static int outlineTickness = 10;
        public static int offsetX = (int)screenWidth / 2;
        public static int offsetY = (int)screenHeight / 2;
        // Ellips C
        public static int unrestrictedWidth = (int)screenWidth / 2 - unrestrictedTickness;
        public static int unrestrictedHeight = (int)screenHeight / 2 - unrestrictedTickness;
        public static int unrestrictedTickness = 100;
        public static int unrestrictedOffsetX = (int)screenWidth / 2 - unrestrictedTickness;
        public static int unrestrictedOffsetY = (int)screenHeight / 2 - unrestrictedTickness;
    }
}
