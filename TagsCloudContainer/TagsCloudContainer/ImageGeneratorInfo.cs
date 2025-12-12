using System.Drawing;

namespace TagsCloudContainer;

public class ImageGeneratorInfo
{
    public Color TextColor { get; set; }
    public Color BackgroundColor { get; set; }
    public int FontSize { get; set; }
    public Size ImageSize { get; set; }

    public ImageGeneratorInfo(Color? textColor = null, Color? backgroundColor = null,
        int fontSize = 14, Size? imageSize = null)
    {
        FontSize = fontSize;
        TextColor = textColor ?? Color.Black;
        BackgroundColor = backgroundColor ?? Color.White;
        ImageSize = imageSize ?? new Size(800, 600);
    }
}
