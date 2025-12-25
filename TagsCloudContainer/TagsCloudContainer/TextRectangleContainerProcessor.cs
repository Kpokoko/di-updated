using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class TextRectangleContainerProcessor
{
    private readonly IFileHandler _fileHandler;
    private readonly ILayouter _layouter;
    private readonly RectangleSizeCalculator _rectangleSizeCalculator;
    
    public TextRectangleContainerProcessor(IFileHandler fileHandler, ILayouter layouter,
        RectangleSizeCalculator calculator)
    {
        this._fileHandler = fileHandler;
        this._layouter = layouter;
        this._rectangleSizeCalculator = calculator;
    }
    
    public IEnumerable<TextRectangleContainer?> ProcessFile(string path, IWordMeasurer wordMeasurer)
    {
        var redData = _fileHandler.HandleFile(path);

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