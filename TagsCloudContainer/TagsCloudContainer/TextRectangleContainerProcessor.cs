using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class TextRectangleContainerProcessor
{
    private readonly IFileReader _fileReader;
    private readonly ILayouter _layouter;
    private readonly RectangleSizeCalculator _rectangleSizeCalculator;
    private readonly BoringWordsProcessor _boringWordsProcessor;
    
    public TextRectangleContainerProcessor(IFileReader fileReader, ILayouter layouter,
        RectangleSizeCalculator calculator, BoringWordsProcessor boringWordsProcessor)
    {
        this._fileReader = fileReader;
        this._layouter = layouter;
        this._rectangleSizeCalculator = calculator;
        this._boringWordsProcessor = boringWordsProcessor;
    }
    
    public IEnumerable<TextRectangleContainer?> ProcessFile(string path, IWordMeasurer wordMeasurer)
    {
        var readData = _boringWordsProcessor.WordsToLowerAndRemoveBoringWords(_fileReader.ReadFile(path));

        foreach (var wordInfo in readData)
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