using System.Collections.Generic;
using Cosmos.System.Graphics;
using Vermin.Drivers;

namespace Vermin.Components
{
    public class TaskbarButton : Button
    {
        public TaskbarButton(Taskbar taskbar, string text, VGAColor backColor, VGAColor foreColor)
            : base(text, 0, 0, 0, backColor, foreColor)
        {
            Text = text;

            var list = taskbar.Buttons;

            X = list.Count == 0 ? taskbar.DefaultPadding : list[list.Count].Width + taskbar.DefaultPadding * 2;
            Y = taskbar.Y;

            Height = taskbar.Height;

            ForeColor = foreColor;
            BackColor = backColor;
        }
    }

    public class Taskbar : Component
    {
        public int Y, Width = Display.Width, Height = 15, DefaultPadding = 5;
        public VGAColor Color = VGAColor.Blue;

        public List<Button> Buttons = new();

        public Taskbar()
        {
            Y = Display.Height - Height;
        }

        public override void Draw()
        {
            Display.DrawRectangle(0, Y, Width, Height, Color);

            foreach (var b in Buttons)
                b.Draw();
        }

        public override void Update() { }
    }
}
