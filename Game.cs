using ConsoleTetris.Objects;
using ConsoleTetris.Objects.Blocks;
using System.Diagnostics;

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
            Block? curBlock = null;
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
                Block iBlock = new I_Block(
                    parent: board,
                    offsetX: 9,
                    offsetY: 0,
                    width: 1,
                    height: 1,
                    priority: 1,
                    isVisible: true,
                    title: "IBlock"
                );
                curBlock = iBlock;


                Block iBlock2 = new I_Block(
                    parent: board,
                    offsetX: 9,
                    offsetY: 0,
                    width: 1,
                    height: 1,
                    priority: 1,
                    isVisible: false,
                    title: "IBlock"
                ); 
                next = iBlock2;

                controllingBlock = true;
            }

            // Good thread for most closely simulate consistent gravity
            void GameGravity()
            {
                while (gameRunning)
                {
                    if (curBlock != null)
                    {
                        int lowestPoint = curBlock.GetLowestPoint();

                        if (Block.CheckBlockUnder(curBlock) || lowestPoint == 23)
                        {
                            // Then change block
                            curBlock = next;
                            curBlock.IsVisible = true;

                            // Create the next block
                            Block iBlock = new I_Block(
                                parent: board,
                                offsetX: 9,
                                offsetY: 0,
                                width: 1,
                                height: 1,
                                priority: 1,
                                isVisible: false,
                                title: "IBlock"
                            );
                            next = iBlock;

                            CheckLines();
                        }

                        curBlock.OffsetY += 1;
                        VirtualScreen.Draw(true);
                        lastTick = DateTime.Now;
                    }

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
                            case (ConsoleKey.D):
                                // Check all blocks positions to the right if its available
                                bool canMoveRight = Block.CheckSide(curBlock, 1);

                                if (canMoveRight)
                                {
                                    curBlock.OffsetX += 2;
                                }
                                break;
                            case (ConsoleKey.A):
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
                            case (ConsoleKey.R):
                                curBlock.Orientation += 90; // This automatically handles wrapping due to the setter's logic
                                break;
                        }
                        //VirtualScreen.Draw();
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
