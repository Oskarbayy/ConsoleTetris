using ConsoleTetris.Objects;
using ConsoleTetris.Objects.Blocks;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleTetris
{
    class Game
    {
        static void Main(string[] args)
        {
            // Entry Point
            int score = 0;
            bool gameRunning = true;
            bool controllingBlock = false;
            Block curBlock = null;
            Block? next = null;

            // Create objects (Load game)
            Board board = new Board(2, 1, 22, 24, 1, true, "Board");

            NextBlock nextBlock = new NextBlock(board, 27, 1, 14, 7, 1, true, "Next");
            ScoreBlock scoreBlock = new ScoreBlock(board, 27, 9, 20, 3, 1, true, "Score");

            // Game Loop Settings
            int tickRate = 10;
            DateTime lastTick = DateTime.Now;

            Console.CursorVisible = false;
            VirtualScreen.Draw(true);

            while (gameRunning)
            {
                // Start game
                // Refresh Screen

                // Create a block
                if (!controllingBlock)
                {
                    I_Block iBlock = new I_Block(board, 9, 0, 1, 1, 1, true, "IBlock1");
                    curBlock = iBlock;


                    I_Block iBlock2 = new I_Block(board, 9, 0, 1, 1, 1, false, "IBlock1");

                    next = iBlock2;

                    controllingBlock = true;
                }

                // Control
                if (Console.KeyAvailable) // if key has been pressed
                {
                    var key = Console.ReadKey(true).Key;
                    ClearConsoleReadKeyQueue();

                    switch (key)
                    {
                        case (ConsoleKey.RightArrow):
                            // Check all blocks positions to the right if its available
                            bool canMoveRight = Block.CheckSide(curBlock, 1);

                            if (canMoveRight)
                            {
                                curBlock.OffsetX += 2;
                                VirtualScreen.Draw(true);
                            }
                            break;
                        case (ConsoleKey.LeftArrow):
                            bool canMoveLeft = Block.CheckSide(curBlock, -1);

                            if (canMoveLeft)
                            {
                                curBlock.OffsetX -= 2;
                                VirtualScreen.Draw(true);
                            }
                            break;
                        case (ConsoleKey.Spacebar):
                            // Spacebar pressed now find the lowest point and calculate to set it to the ground 
                            Block.QuickPlace(curBlock);
                            VirtualScreen.Draw(true);
                            break;
                    }
                }

                // Check Clock for Gravity
                if ((DateTime.Now - lastTick).TotalMilliseconds >= tickRate && curBlock != null)
                {
                    // About to move block down
                    int lowestPoint = curBlock.GetLowestPoint();

                    if (Block.CheckBlockUnder(curBlock) || lowestPoint == 23) // FIX Block.CheckBlockUnder DOESNT WORK 
                    {
                        curBlock = next;
                        curBlock.IsVisible = true;

                        I_Block iBlock = new I_Block(board, 9, 0, 1, 1, 1, false, "IBlock1");
                        next = iBlock;
                    }

                    curBlock.OffsetY += 1;
                    VirtualScreen.Draw(true);
                    lastTick = DateTime.Now;
                }

                // Setup Next Block

                // Brief pause for the program
            }

            static void ClearConsoleReadKeyQueue()
            {
                while (Console.KeyAvailable) // While there are keys in the queue
                {
                    Console.ReadKey(true); // Read and discard the key press without displaying it
                }
            }
        }
    }
}
