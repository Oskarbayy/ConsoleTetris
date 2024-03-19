using ConsoleTetris.Objects;
using ConsoleTetris.Objects.Blocks;

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
            int tickRate = 100;
            DateTime lastTick = DateTime.Now;

            Console.CursorVisible = false;
            VirtualScreen.Draw(true);

            // START THE GAME //

            // gravity Thread to consistently move block down
            Thread gravityThread = new Thread(new ThreadStart(GameGravity));
            gravityThread.Start();

            // State thread to control / check the game state
            Thread stateThread = new Thread(new ThreadStart(GameState));
            stateThread.Start();

            // Create current block and next block
            if (!controllingBlock)
            {
                I_Block iBlock = new I_Block(board, 9, 0, 1, 1, 1, true, "IBlock1");
                curBlock = iBlock;


                I_Block iBlock2 = new I_Block(board, 9, 0, 1, 1, 1, false, "IBlock1");

                next = iBlock2;

                controllingBlock = true;
            }

            // Good thread for most closely simulate consistent gravity
            void GameGravity()
            {
                while (gameRunning)
                {
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

                            CheckLines();
                        }

                        curBlock.OffsetY += 1;
                        VirtualScreen.Draw(true);
                        lastTick = DateTime.Now;
                    }

                    // Giving the CPU a little less work
                    Thread.Sleep(10);
                }
            }

            // Check game state like if theres any lines on the board
            void GameState()
            {
                // Key presses only and input the actual board line checking as a function in game gravity
                while (gameRunning)
                {
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
                                }
                                break;
                            case (ConsoleKey.LeftArrow):
                                bool canMoveLeft = Block.CheckSide(curBlock, -1);

                                if (canMoveLeft)
                                {
                                    curBlock.OffsetX -= 2;
                                }
                                break;
                            case (ConsoleKey.Spacebar):
                                // Spacebar pressed now find the lowest point and calculate to set it to the ground 
                                Block.QuickPlace(curBlock);
                                break;
                        }
                    }

                    Thread.Sleep(10);
                }
            }

            void CheckLines()
            {

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
