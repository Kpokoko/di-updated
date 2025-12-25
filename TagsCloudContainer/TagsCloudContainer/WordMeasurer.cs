using System.Drawing;

namespace TagsCloudContainer;

public class WordMeasurer : IWordMeasurer
{
    private readonly Graphics _graphics;
    private readonly Font _font;
    
    public WordMeasurer(Graphics graphics, Font font)
    {
        this._graphics = graphics;
        this._font = font;
    }
    
    public Size Measure(string word, float scale)
    {
        var tempFont = GetFont(scale);
        var sizeF = _graphics.MeasureString(word, tempFont);
        tempFont.Dispose();
        
        return new Size(
            (int)Math.Ceiling(sizeF.Width),
            (int)Math.Ceiling(sizeF.Height)
        );
    }

    public Font GetFont(float scale)
    {
        return new Font(_font.FontFamily, _font.Size * scale, _font.Style);
    }
}