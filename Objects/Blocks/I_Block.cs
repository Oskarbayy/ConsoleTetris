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

            switch (this.Orientation)
            {
                case 0:
                case 180:
                    for (int i = 0; i < 4; i++)
                    {
                        VirtualScreen.SetPixel(Parent.OffsetX + OffsetX, Parent.OffsetY + OffsetY + i, '█', this);
                        VirtualScreen.SetPixel(Parent.OffsetX + OffsetX + 1, Parent.OffsetY + OffsetY + i, '█', this);
                    }
                    break;
                case 90:
                case 270:
                    for (int i = 0; i < 8; i++)
                    {
                        VirtualScreen.SetPixel(Parent.OffsetX + OffsetX + i - 2, Parent.OffsetY + OffsetY + 1, '█', this);
                        //VirtualScreen.SetPixel(Parent.OffsetX + OffsetX + i, Parent.OffsetY + OffsetY + 1, '█', this);
                    }
                    break;
            }

            Console.ResetColor();
        }

    }
}
