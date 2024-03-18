using ConsoleTetris.Objects;

namespace ConsoleTetris.Interfaces
{
    public abstract class Drawable : Utility
    {
        // Properties all drawables should have
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Priority { get; set; }
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public Board? Parent { get; set; }

        public Drawable(Board? parent, int offsetX, int offsetY, int width,
        int height, int priority, bool isVisible, string title)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            Width = width;
            Height = height;
            Priority = priority;
            IsVisible = isVisible;
            Title = title;
            Parent = parent;
        }

        // Abstract method to be implemented by derived classes (Any classes that should be able to be drawn)
        public abstract void Draw();
    }
}
