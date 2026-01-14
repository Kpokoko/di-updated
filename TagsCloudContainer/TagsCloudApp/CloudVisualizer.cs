using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer;

namespace TagsCloudApp;

public class CloudVisualizer
{
    private static Bitmap _bitmap;
    public static Graphics Graphics { get; private set; }

    public static void PrepareGraphics(Size imageSize)
    {
        _bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        Graphics = Graphics.FromImage(_bitmap);
    }
    
    public static void Draw(TextRectangleContainerProcessor processor,
        ImageGeneratorInfo info,
        string inputFile,
        IWordMeasurer wordMeasurer)
    {
        var path = info.OutputFileName;
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        Graphics.Clear(info.BackgroundColor);

        using var brush = new SolidBrush(info.TextColor);

        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        foreach (var container in processor.ProcessFile(inputFile, wordMeasurer))
        {
            if (container is null)
                continue;
            var rect = container.Rectangle;
            using var font =
                new Font(info.Font.FontFamily, info.Font.Size * container.FontSizeScale, info.Font.Style);
            Graphics.DrawString(container.Text, font, brush, rect, format);
        }

        _bitmap.Save(path);
    }

}