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
        public static int minRadius = 1;
        public static int radius = Math.Min((int)screenWidth, (int)screenHeight);
        public static int minThickness = 0;
        public static double minOpacity = 0.2;
        public static double maxOpacity = 1;
        // Ellips A
        public static int tickness = 5;
        // Ellips B
        private static int outlineMaxRadius;
        public static int outlineThickness = 10;
        public static int offsetX = (int)screenWidth;
        public static int offsetY = (int)screenHeight;
        // Ellips C
        private static int unrestrictedWidth;
        private static int unrestrictedHeight;
        public static int unrestrictedThickness = 100;
        private static int unrestrictedOffsetX;
        private static int unrestrictedOffsetY;
        // Color Filter
        public static int maxFilterSize = (int)screenWidth;
        public static int maxFilterHight = (int)screenHeight;
        // Counter strafe
        public static int minCSPressDuration = 0;
        public static int maxCSPressDuration = 300;

        public static int GetOutlineMaxRadius(int currentThickness)
        {
            outlineMaxRadius = radius - currentThickness;
            return outlineMaxRadius;
        }

        public static int GetUnrestrictedWidth(int currentThickness)
        {
            unrestrictedWidth = (int)screenWidth - currentThickness;
            return unrestrictedWidth;
        }

        public static int GetUnrestrictedHeight(int currentThickness)
        {
            unrestrictedHeight = (int)screenHeight - currentThickness;
            return unrestrictedHeight;
        }

        public static int GetUnrestrictedOffsetX(int currentHeight, int currentThickness)
        {
            unrestrictedOffsetX = (int)screenWidth - currentHeight - currentThickness;
            return unrestrictedOffsetX;
        }

        public static int GetUnrestrictedOffsetY(int currentWidth, int currentThickness)
        {
            unrestrictedOffsetY = (int)screenHeight - currentWidth - currentThickness;
            return unrestrictedOffsetY;
        }
    }
}
