using ConsoleTetris.Interfaces;
using System.Diagnostics;

namespace ConsoleTetris
{
    public class Utility
    {
        // Unicodes
        public const char Horizontal = '\u2500'; // Public?
        const char Vertical = '\u2502';
        const char topRight = '\u2510';
        const char topLeft = '\u250C';
        const char bottomRight = '\u2518';
        const char bottomLeft = '\u2514';

        const char downwardT = '\u252C';
        const char upwardT = '\u2534';
        const char rightwardT = '\u251C';
        const char leftwardT = '\u2524';
        const char cross = '\u253C';

        public static char GetIntersectionCharacter(char[,] buffer, int x, int y, int width, int height, char c)
        {
            // Bunch of bullshit to make corners and stuff
            bool up = y > 0 && (buffer[x, y - 1] == Vertical || buffer[x, y - 1] == downwardT || buffer[x, y - 1] == upwardT || buffer[x, y - 1] == cross);
            bool down = y < height - 1 && (buffer[x, y + 1] == Vertical || buffer[x, y + 1] == downwardT || buffer[x, y + 1] == upwardT || buffer[x, y + 1] == cross);
            bool left = x > 0 && (buffer[x - 1, y] == Horizontal || buffer[x - 1, y] == leftwardT || buffer[x - 1, y] == rightwardT || buffer[x - 1, y] == cross);
            bool right = x < width - 1 && (buffer[x + 1, y] == Horizontal || buffer[x + 1, y] == leftwardT || buffer[x + 1, y] == rightwardT || buffer[x + 1, y] == cross);

            if (up && down && left && right) return cross; // cross
            if (up && down && left) return leftwardT; // leftward T
            if (up && down && right) return rightwardT; // rightward T
            if (up && left && right) return upwardT; // upward T
            if (down && left && right) return downwardT; // downward T

            // If it's a corner without a direct T or cross connection
            if (up && right) return bottomLeft;
            if (up && left) return bottomRight;
            if (down && right) return topLeft;
            if (down && left) return topRight;

            return c; // return the original character if no connection is found
        }

        public void AddText(int offsetX, int offsetY, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                VirtualScreen.SetPixel(offsetX + i, offsetY, c);
            }
        }

        public static void DrawBox(int offsetX, int offsetY, int width, int height)
        {
            // Draw top and bottom
            for (int x = offsetX; x < offsetX + width; x++)
            {
                VirtualScreen.SetPixel(x, offsetY, Horizontal); // Top
                VirtualScreen.SetPixel(x, offsetY + height - 1, Horizontal); // Bottom 
            }

            // Draw walls left and right
            for (int y = 0; y < height; y++)
            {
                VirtualScreen.SetPixel(offsetX, y + offsetY, Vertical);
                VirtualScreen.SetPixel(offsetX + width - 1, y + offsetY, Vertical);
            }

            // Set corners
            VirtualScreen.SetPixel(offsetX, offsetY, topLeft);
            VirtualScreen.SetPixel(offsetX + width - 1, offsetY, topRight);
            VirtualScreen.SetPixel(offsetX, offsetY + height - 1, bottomLeft);
            VirtualScreen.SetPixel(offsetX + width - 1, offsetY + height - 1, bottomRight);
        }

        public static void DrawWindow(Drawable win)
        {
            int offsetX = win.OffsetX;
            int offsetY = win.OffsetY;
            int width = win.Width;
            int height = win.Height;

            Console.ForegroundColor = ConsoleColor.DarkGray;

            // Draw top and bottom
            for (int x = offsetX+1; x < offsetX + width-2; x+=2)
            {
                if ((x-1)%4 == 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                VirtualScreen.SetPixel(x, offsetY, '█'); // Top
                VirtualScreen.SetPixel(x+1, offsetY, '█'); // Top

                VirtualScreen.SetPixel(x, offsetY + height - 1, '█'); // Bottom 
                VirtualScreen.SetPixel(x+1, offsetY + height - 1, '█'); // Bottom 
            }
            Console.ResetColor();

            // Draw walls left and right
            for (int y = 0; y < height; y++)
            {
                VirtualScreen.SetPixel(offsetX, y + offsetY, '▐');

                VirtualScreen.SetPixel(offsetX + width - 1, y + offsetY, '▌');
            }

            Console.ResetColor();
        }
    }
}
