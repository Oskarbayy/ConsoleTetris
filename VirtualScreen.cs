﻿using ConsoleTetris.Interfaces;
using ConsoleTetris.Objects;
using ConsoleTetris.Structs;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ConsoleTetris
{
    class VirtualScreen : Utility
    {
        static int Width = 100;
        static int Height = 30;

        static ConsolePixel[,] buffer = new ConsolePixel[Width, Height];

        // Windows Loaded
        public static List<Board> windows = new List<Board>();

        public static void LoadWindow(Board win)
        {
            if (win.IsVisible)
            {
                // Create window box and loop through components and draw components?

                win.Draw();

                // Loop through components
                foreach (Drawable com in win.Components)
                {
                    if (com.IsVisible)
                    {
                        if (com is Board window)
                        {
                            window.Components.Clear(); // Components is a 'List<List<Interactable>>' type

                            LoadWindow(window);
                        }

                        com.Draw();
                    }
                }
            }
        }

        public static void Draw(bool showScreen = false)
        {
            Clear();

            // Sort windows
            windows.Sort((window1, window2) => window1.Priority.CompareTo(window2.Priority));

            // Draw on the virtual screen first and show later
            foreach (Board win in windows)
            {
                LoadWindow(win);
            }

            if (showScreen)
                ShowScreen();
        }

        static void ShowScreen()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.ForegroundColor = buffer[x, y].ForegroundColor;
                    Console.BackgroundColor = buffer[x, y].BackgroundColor;
                    Console.Write(buffer[x, y].Character);
                }
                Console.ResetColor();

                if (y < Height - 1)
                    Console.WriteLine();
            }
        }


        public static void SetPixel(int x, int y, char c, Block? block = null)
        {
            ConsoleColor foreground = Console.ForegroundColor;
            ConsoleColor background = Console.BackgroundColor;

            if (block != null)
            {
                block.Positions.Add((x, y));
            }

            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                buffer[x, y] = new ConsolePixel(c, foreground, background);
            }
            else
            {
                Debug.WriteLine(x + ", " + y + " : doesn't exist inside the screen frame!");
            }
        }


        public static void Clear()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    buffer[x, y] = new ConsolePixel(' ', Console.ForegroundColor, Console.BackgroundColor);
                }
            }
        }

    }
}
