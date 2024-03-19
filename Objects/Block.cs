using ConsoleTetris.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Objects
{
    public abstract class Block : Drawable
    {
        static List<Block> Blocks = new List<Block>();

        public List<(int X, int Y)> Positions = new List<(int X, int Y)>();
        
        public (int X, int Y) Pivot { get; set; }
        
        public Block(Board parent, int offsetX, int offsetY, int width, int height, int priority, bool isVisible, string title) 
            : base(parent, offsetX, offsetY, width, height, priority, isVisible, title) 
        {
            Blocks.Add(this);
        }

        // add a functoin that checks all blocks pixels and checks whats the lowest point on the Y-axis

        static public bool CheckBlockUnder(Block curBlock)
        {
            List<(int X, int Y)> lowestPositions = GetLowestPositions(curBlock);

            // Check if the list is empty and return false if it is.
            if (!lowestPositions.Any())
            {
                return false;
            }

            foreach (var pos1 in lowestPositions)
            {
                foreach (var block in Blocks.Where(b => b != curBlock))
                {
                    if (block.Positions.Any(pos2 => pos1.X == pos2.X && pos1.Y + 1 == pos2.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        static public bool CheckSide(Block curBlock, int num)
        {
            foreach (var pos1 in curBlock.Positions)
            {
                // Start with checking the border since that requires less
                if (pos1.X + num < 3 || pos1.X + num > 22)
                    return false;

                foreach (var block in Blocks.Where(b => b != curBlock))
                {
                    // check if block position is right beside the curblock pos
                    if (block.Positions.Any(pos2 => pos1.X+num == pos2.X && pos1.Y == pos2.Y)) 
                    {
                        return false;
                    }
                }
            }


            return true;
        }

        public void Rotate()
        {
            List<(int X, int Y)> newPositions = new List<(int X, int Y)>();

            foreach (var pos in this.Positions)
            {
                // Translate cell position relative to the pivot
                int relativeX = pos.X - Pivot.X;
                int relativeY = pos.Y - Pivot.Y;

                // Apply rotation formula
                int rotatedX = relativeY;
                int rotatedY = -relativeX;

                // Translate back and add to new positions
                newPositions.Add((rotatedX + Pivot.X, rotatedY + Pivot.Y));
            }

            Positions = newPositions;
        }

        static public bool QuickPlace(Block curBlock)
        {
            int maxDropDistance = 23;

            foreach (var pos in curBlock.Positions)
            {
                int dropDistance = 0;
                for (int y = pos.Y + 1; y <= 23; y++) // Assuming 23 is the maximum height your board can have
                {
                    // Check if the current position is occupied, breaking before incrementing dropDistance
                    if (IsPositionOccupied(pos.X, y, curBlock))
                    {
                        // The current y is occupied, so we break before this position
                        break;
                    }
                    dropDistance++;
                }

                maxDropDistance = Math.Min(maxDropDistance, dropDistance);
            }

            if (maxDropDistance != 24)
            {
                curBlock.OffsetY += maxDropDistance;
                return true;
            }

            return false;
        }

        static bool IsPositionOccupied(int x, int y, Block curBlock)
        {
            foreach (var block in Blocks.Where(b => b != curBlock))
            {
                if (block.Positions.Any(pos => pos.X == x && pos.Y == y))
                {
                    return true; // Position is occupied
                }
            }
            return false;
        }



        static public List<(int X, int Y)> GetLowestPositions(Block curBlock)
        {
            if (curBlock.Positions.Count == 0)
            {
                return new List<(int X, int Y)>();
            }

            int maxY = curBlock.Positions.Max(pos => pos.Y);
            Debug.WriteLine(maxY);

            return curBlock.Positions.Where(pos => pos.Y == maxY).ToList();
        }



        public int GetLowestPoint()
        {
            int highestY = 0;
            foreach (var item in Positions)
            {
                int x = item.X;
                int y = item.Y;

                if (y > highestY)
                {
                    highestY = y;
                }
            }

            return highestY;
        }

        public abstract override void Draw();
    }
}
