using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class TextRectangleContainerProcessor
{
    private readonly IFileHandler _fileHandler;
    private readonly ILayouter _layouter;
    private readonly RectangleSizeCalculator _rectangleSizeCalculator;
    private readonly List<string> _banWords;
    
    public TextRectangleContainerProcessor(IFileHandler fileHandler, ILayouter layouter,
        RectangleSizeCalculator calculator, List<string> banWords)
    {
        this._fileHandler = fileHandler;
        this._layouter = layouter;
        this._rectangleSizeCalculator = calculator;
        this._banWords = banWords;
    }
    
    public IEnumerable<TextRectangleContainer?> ProcessFile(string path, IWordMeasurer wordMeasurer)
    {
        var redData = _fileHandler.HandleFile(path, _banWords);

        foreach (var wordInfo in redData)
        {
            var rectSize = _rectangleSizeCalculator.CalculateNextRectangleSize(wordInfo, wordMeasurer, out var scale);
            if (rectSize == Size.Empty)
            {
                yield return null;
                continue;
            }

            var rect = _layouter.PutNextRectangle(rectSize);
             yield return new TextRectangleContainer(rect, wordInfo.Key, scale);
        }
    }
}