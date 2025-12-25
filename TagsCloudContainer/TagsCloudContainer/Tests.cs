using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudContainer;

[TestFixture]
public class Tests
{
    private Point _center;
    private Size _imageSize;
    private string _filePath = "TestData";
    private IContainer _container;
    
    [SetUp]
    public void Setup()
    {
        _center = new Point(400, 400);
        _imageSize = new Size(800, 800);
        using var bitmap = new Bitmap(_imageSize.Width, _imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        var font = new Font(FontFamily.GenericSansSerif, 20);
        
        _container = ContainerComposer.Compose(_center, _imageSize, graphics, font);
    }
    
    [Test]
    public void IFileHandler_ReadFile_Should_CorrectlyReadText()
    {
        var expectedData = new List<string> { "odin" };
        
        var data = _container
            .Resolve<IFileHandler>()
            .ReadFile(Path.Combine(_filePath,
            "IFileHandler_ReadFile_Should_CorrectlyReadText.txt"));
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void IFileHandler_WordsToLowerAndRemoveBoringWords_Should_CorrectlyProcessText()
    {
        var inputData = new List<string> { "скучное", "слово", "в", "СЛОВО" };
        var expectedData = new[] { "слово", "скучное" };
        
        var data = _container
            .Resolve<IFileHandler>()
            .WordsToLowerAndRemoveBoringWords(inputData);
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void IFIleHandler_HandleFile_Should_CorrectlyProcessText()
    {
        var expectedData = new[] { "слово", "в", "скучное" };

        var data = _container
            .Resolve<IFileHandler>()
            .HandleFile(Path.Combine(_filePath,
            "IFIleHandler_HandleFile_Should_CorrectlyProcessText.txt"));
        
        data.Keys.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void IFIleHandler_HandleFile_Should_RemoveBoringWords()
    {
        var expectedData = new[] { "слово", "скучное" };

        var data = _container
            .Resolve<IFileHandler>()
            .HandleFile(Path.Combine(_filePath,
            "IFIleHandler_HandleFile_Should_CorrectlyProcessText.txt"));
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void VisualizationComposer_Should_ComposeDataCorrectly()
    {
        var path = Path.Combine(_filePath,
            "VisualizationComposer_Should_ComposeDataCorrectly.txt");
        var expectedData = PrepareComposerExpectedData(path);
        
        var resultData = _container
            .Resolve<TextRectangleContainerProcessor>()
            .ProcessFile(path, _container.Resolve<IWordMeasurer>());
        
        resultData.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void DIContainer_Resolve_CircularCloudLayouter_Test()
    {
        var layouter = _container.Resolve<ILayouter>();
        layouter.Should().NotBeNull();
    }

    private TextRectangleContainer[] PrepareComposerExpectedData(string path)
    {
        var data = _container
            .Resolve<IFileHandler>()
            .HandleFile(path);
        var containers = new TextRectangleContainer[data.Count];
        var rect1 = new Rectangle(new Point(340, 360), new Size(120, 80));
        var rect2 = new Rectangle(new Point(357, 320), new Size(60, 40));
        var rect3 = new Rectangle(new Point(417, 337), new Size(30, 20));
        containers[0] = new TextRectangleContainer(rect1, "скучное", 1);
        containers[1] = new TextRectangleContainer(rect2, "слово",0.5f );
        containers[2] = new TextRectangleContainer(rect3, "в",0.25f);
        return containers;
    }
}