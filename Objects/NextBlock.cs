using ConsoleTetris.Interfaces;

namespace ConsoleTetris.Objects
{
    class NextBlock : Drawable
    {
        public NextBlock(Board parent, int offsetX, int offsetY, int width, int height, int priority, bool isVisible, string title)
            : base(parent, offsetX, offsetY, width, height, priority, isVisible, title)
        {
            Parent.Components.Add(this);
        }

        public override void Draw()
        {
            AddText(OffsetX + 1, OffsetY - 1, Title);
            DrawBox(OffsetX, OffsetY, Width, Height);
        }
    }
}
