namespace ConsoleTetris.Objects.Blocks
{
    class I_Block : Block
    {

        public I_Block(Board parent, int offsetX, int offsetY, int width, int height, int priority, bool isVisible, string title)
            : base(parent, offsetX, offsetY, width, height, priority, isVisible, title)
        {

            parent.Components.Add(this);
        }

        public override void Draw()
        {
            this.Positions.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < 4; i++)
            {
                VirtualScreen.SetPixel(Parent.OffsetX + OffsetX, Parent.OffsetY + OffsetY + i, '█', this);
                VirtualScreen.SetPixel(Parent.OffsetX + OffsetX + 1, Parent.OffsetY + OffsetY + i, '█', this);

            }

            Console.ResetColor();

            /* var check = this.Positions;
            var checkNothing = Block.GetLowestPositions(this); */
        }
    }
}
