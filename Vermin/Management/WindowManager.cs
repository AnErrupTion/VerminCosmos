using Vermin.Components;
using System.Collections.Generic;

namespace Vermin.Management
{
    public class WindowManager
    {
        private static readonly List<Window> OpenedWindows = new List<Window>();

        public static void Open(Window w)
        {
            OpenedWindows.Add(w);
            w.Open();
        }

        public static void Close(Window w)
        {
            // TODO : Fix (issue with Cosmos)
            /*if (!OpenedWindows.Contains(w))
                return;*/

            w.Close();
            //OpenedWindows.Remove(w);
        }

        public static void UpdateAll()
        {
            foreach (var w in OpenedWindows)
            {
                w.Draw();
                w.Update();
            }
        }
    }
}
