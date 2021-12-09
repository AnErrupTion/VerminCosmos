using Cosmos.System;
using Cosmos.System.Graphics;

namespace Vermin.Drivers
{
    public class Mouse
    {
        public static int X { get => (int) MouseManager.X; set => MouseManager.X = (uint) value; }
        
        public static int Y { get => (int) MouseManager.Y; set => MouseManager.Y = (uint) value; }

        public static MouseState State { get => MouseManager.MouseState; }

        public static int OldX, OldY;

        public static void Initialize()
        {
            MouseManager.ScreenWidth = (uint) Display.Width;
            MouseManager.ScreenHeight = (uint) Display.Height;
        }

        public static void Draw(VGAColor c)
        {
            var x = X;
            var y = Y;

            DrawMouse(OldX, OldY, Display.BackColor);
            DrawMouse(x, y, c);

            OldX = x;
            OldY = y;
        }

        private static void DrawMouse(int x, int y, VGAColor c)
        {
            // First pixel
            Display.SetPixel(x, y, c);

            // 2x2 square
            Display.SetPixel(x + 1, y, c);
            Display.SetPixel(x + 1, y + 1, c);
            Display.SetPixel(x, y + 1, c);

            // Cursor
            Display.SetPixel(x + 2, y, c);
            Display.SetPixel(x + 2, y + 2, c);
            Display.SetPixel(x, y + 2, c);
        }
    }
}
