using System.Drawing;

namespace TagsCloudContainer;

public interface IWordMeasurer
{
    public Size Measure(string word, float scale);
    public Font GetFont(float scale);
}