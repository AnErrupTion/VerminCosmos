using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using Vermin.Drivers;
using Vermin.Management;

namespace Vermin.Components
{
    public class Window : Component
    {
        public string Title;

        public int X, Y, OldX, OldY, Width, Height, TitlebarHeight = 15, Id;

        public VGAColor TitlebarColor;
        
        public bool Held = false, Opened = false;
        
        public Button CloseBtn;

        public Window(string title, int x, int y, int width, int height, VGAColor titlebarColor)
        {
            Title = title;

            X = x;
            Y = y;

            Width = width;
            Height = height;

            TitlebarColor = titlebarColor;

            Id = new Random().Next();
        }

        public void Open()
        {
            Opened = true;
        }

        public void Close()
        {
            Opened = false;
            DrawWindow(OldX, OldY, Display.BackColor, Display.BackColor);
        }

        public override void Draw()
        {
            if (Opened)
            {
                var x = X;
                var y = Y;

                DrawWindow(OldX, OldY, Display.BackColor, Display.BackColor);
                DrawWindow(x, y, TitlebarColor, VGAColor.White);

                CloseBtn = new Button("X", X + Width - TitlebarHeight, Y, TitlebarHeight, VGAColor.Red, VGAColor.White);
                CloseBtn.Draw();

                OldX = x;
                OldY = y;
            }
        }

        public override void Update()
        {
            if (Opened)
            {
                if (CloseBtn.IsClicked())
                    WindowManager.Close(this);

                if (!Held && Mouse.State == MouseState.Left && IsInBounds())
                    Held = true;

                if (Held)
                {
                    Move(X - Mouse.OldX + Mouse.X, Y - Mouse.OldY + Mouse.Y);
                    Held = Mouse.State == MouseState.Left;
                }
            }
        }

        private void DrawWindow(int x, int y, VGAColor bar, VGAColor body)
        {
            Display.DrawRectangle(x, y, Width, Height, bar);
            Display.DrawString(Title, x + 5, y, body, VGAFont.Font8x8);
            Display.DrawRectangle(x, y + TitlebarHeight, Width, Height, body);
        }

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsInBounds()
        {
            return Mouse.X <= X + Width && Mouse.X >= X &&
                Mouse.Y <= Y + TitlebarHeight && Mouse.Y >= Y;
        }
    }
}
