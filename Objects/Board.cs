using ConsoleTetris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Objects
{
    public class Board : Drawable
    {
        // Properties all windows should have //
        public List<Board> ChildWindows { get; } = new List<Board>();   // All windows made under this window should be added here
        public List<Drawable> Components { get; } = new List<Drawable>(); // everything that isnt a window under the window should be added here


        public Board(int offsetX, int offsetY, int width, int height, int priority, bool isVisible, string title)
            : base(null, offsetX, offsetY, width, height, priority, isVisible, title) 
        {
            VirtualScreen.windows.Add(this);
        }

        public override void Draw()
        {
            DrawWindow(this);
        }
    }
}
