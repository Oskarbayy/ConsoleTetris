using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Structs
{
    public struct Area
    {
        public Position TopLeft { get; set; }
        public Position BottomRight { get; set; }

        public Area(Position topLeft, Position bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}