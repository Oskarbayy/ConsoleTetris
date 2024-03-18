using ConsoleTetris.Objects;

namespace ConsoleTetris.Structs
{
    public struct ConsolePixel
    {
        public char Character { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public Block? Block { get; set; }

        public ConsolePixel(char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;

            // get block.Positions and set them all to these points and reset them every frame?
        }
    }
}
