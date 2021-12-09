using Cosmos.System.Graphics;
using Cosmos.HAL;

namespace Vermin.Drivers
{
    public class Display
    {
        public static int Width { get => VGADriverII.Width; }

        public static int Height { get => VGADriverII.Height; }

        public static VGAColor BackColor = VGAColor.Gray1;

        public static void Initialize(VGAMode mode)
        {
            VGAGraphics.Initialize(mode);
        }

        public static void SetPixel(int x, int y, VGAColor c)
        {
            if (x > Width || y > Height)
                return;

            VGAGraphics.DrawPixel(x, y, c);
        }

        public static void DrawRectangle(int x, int y, int width, int height, VGAColor c)
        {
            if (x > Width || y > Height)
                return;

            VGAGraphics.DrawFilledRect(x, y, width, height, c);
        }

        public static void DrawString(string text, int x, int y, VGAColor c, VGAFont font)
        {
            if (x > Width || y > Height)
                return;

            VGAGraphics.DrawString(x, y, text, c, font);
        }

        public static void ClearScreen()
        {
            VGAGraphics.Clear(BackColor);
        }
    }
}
