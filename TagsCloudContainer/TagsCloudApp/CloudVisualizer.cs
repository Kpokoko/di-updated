using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer;

namespace TagsCloudApp;

public static class CloudVisualizer
{
    public static void Draw(IEnumerable<TextRectangleContainer> rectangles, ImageGeneratorInfo info)
    {
        var path = info.OutputFileName;
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        using var bmp = new Bitmap(info.ImageSize.Width, info.ImageSize.Height);
        using var graphics = Graphics.FromImage(bmp);

        graphics.Clear(info.BackgroundColor);

        using var font = info.Font;
        using var brush = new SolidBrush(info.TextColor);

        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        foreach (var container in rectangles)
        {
            var rect = container.Rectangle;

            graphics.FillRectangle(Brushes.LightBlue, rect);
            graphics.DrawRectangle(Pens.Black, rect);
            graphics.DrawString(container.Text, font, brush, rect, format);
        }

        bmp.Save(path, ImageFormat.Bmp);
    }
}