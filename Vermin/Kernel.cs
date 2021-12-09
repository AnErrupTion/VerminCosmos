using Vermin.Drivers;
using Vermin.Components;
using Sys = Cosmos.System;
using Console = System.Console;
using Vermin.Management;
using Cosmos.HAL;
using Cosmos.System.Graphics;

namespace Vermin
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Vermin booted successfully.");

            Display.Initialize(VGAMode.Pixel320x200DB);
            Mouse.Initialize();
        }

        private readonly bool Running = true;

        protected override void Run()
        {
            Display.ClearScreen();

            var taskbar = new Taskbar();
            taskbar.Buttons.Add(new TaskbarButton(taskbar, "Shutdown", VGAColor.Orange, VGAColor.White));
            taskbar.Buttons.Add(new TaskbarButton(taskbar, "Window", VGAColor.Pink, VGAColor.White));

            while (Running)
            {
                // Draw desktop components
                WindowManager.UpdateAll();
                taskbar.Draw();

                // Update desktop components states
                if (taskbar.Buttons[0].IsClicked())
                    Power.ACPIShutdown();

                if (taskbar.Buttons[1].IsClicked())
                    WindowManager.Open(new Window("Test", 20, 20, 250, 150, VGAColor.Blue));

                // Draw mouse and update screen
                Mouse.Draw(VGAColor.White);
                VGAGraphics.Display();
            }
        }
    }
}
