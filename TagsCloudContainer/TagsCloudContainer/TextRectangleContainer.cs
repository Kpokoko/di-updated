using System.Drawing;

namespace TagsCloudContainer;

public class TextRectangleContainer
{
    public Rectangle Rectangle { get; private set; }
    public string Text { get; private set; }

    public TextRectangleContainer(Rectangle rectangle, string text)
    {
        Rectangle = rectangle;
        Text = text;
    }
}