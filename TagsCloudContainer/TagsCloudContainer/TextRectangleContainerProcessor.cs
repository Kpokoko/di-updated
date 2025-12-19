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
    
    public List<TextRectangleContainer>ProcessFile(string path)
    {
        var redData = _fileHandler.HandleFile(path);
        var resultData = new List<TextRectangleContainer>();

        foreach (var wordInfo in redData)
        {
            var rectSize = _rectangleSizeCalculator.CalculateNextRectangleSize(wordInfo.Value);
            var rect = _layouter.PutNextRectangle(rectSize);
            resultData.Add(new TextRectangleContainer(rect, wordInfo.Key));
        }

        return resultData;
    }
}