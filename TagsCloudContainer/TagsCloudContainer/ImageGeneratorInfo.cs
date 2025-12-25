using System.Drawing;

namespace TagsCloudContainer;

public class ImageGeneratorInfo
{
    public Color TextColor { get; set; }
    public Color BackgroundColor { get; set; }
    public Font Font { get; set; }
    public Size ImageSize { get; set; }
    public string OutputFileName { get; set; }

    public ImageGeneratorInfo(Color? textColor = null, Color? backgroundColor = null,
        Font? font = null, Size? imageSize = null, string? outputFileName = null)
    {
        Font = font ?? new Font(FontFamily.GenericSansSerif, 60);
        TextColor = textColor ?? Color.Black;
        BackgroundColor = backgroundColor ?? Color.White;
        ImageSize = imageSize ?? new Size(800, 600);
        OutputFileName = outputFileName ?? "output.bmp";
    }
}
