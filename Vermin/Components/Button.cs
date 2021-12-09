using Cosmos.System;
using Cosmos.System.Graphics;
using Vermin.Drivers;

namespace Vermin.Components
{
    public class Button : Component
    {
        public string Text;
        public int X, Y, Width, Height;
        public VGAColor BackColor, ForeColor;

        public Button(string text, int x, int y, int height, VGAColor backColor, VGAColor foreColor)
        {
            Text = text;

            X = x;
            Y = y;

            Height = height;

            BackColor = backColor;
            ForeColor = foreColor;
        }

        public override void Draw()
        {
            Width = 8 * Text.Length + 6;

            Display.DrawRectangle(X, Y, Width, Height, BackColor);
            Display.DrawString(Text, X, Y, ForeColor, VGAFont.Font8x8);
        }

        public override void Update() { }

        private static bool IsPressedOneTime()
        {
            var state = Mouse.State;

            if (state == MouseState.Left)
            {
                while (Mouse.State == state) ;
                return Mouse.State == MouseState.None;
            }

            return false;
        }

        public bool IsClicked()
        {
            return Mouse.X <= X + Width && Mouse.X >= X &&
                Mouse.Y <= Y + Height && Mouse.Y >= Y &&
                IsPressedOneTime();
        }
    }
}
