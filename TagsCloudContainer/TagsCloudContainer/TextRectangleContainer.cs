using System.Drawing;

namespace TagsCloudContainer;

public class TextRectangleContainer
{
    public Rectangle Rectangle { get; private set; }
    public string Text { get; private set; }
    public float FontSizeScale { get; private set; }

    public TextRectangleContainer(Rectangle rectangle, string text, float scale)
    {
        Rectangle = rectangle;
        Text = text;
        FontSizeScale = scale;
    }
}