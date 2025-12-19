using System.Drawing;

namespace TagsCloudContainer;

public class RectangleSizeCalculator
{
    private Size _maxSize;
    
    internal RectangleSizeCalculator(Size startSize) => _maxSize = startSize;

    internal Size CalculateNextRectangleSize(int wordFrequency)
    {
        return _maxSize * wordFrequency;
    }
}