using System.Drawing;
using NUnit.Framework;
using Autofac;
using FluentAssertions;

namespace TagsCloudContainer.Tests;

[TestFixture]
public class VisualizationComposerTests
{
    private Point _center;
    private Size _imageSize;
    private string _filePath = "TestData";
    private IContainer _container;
    private Graphics _graphics;
    private Bitmap _bitmap;
    
    [SetUp]
    public void Setup()
    {
        _center = new Point(400, 400);
        _imageSize = new Size(800, 800);
        _bitmap = new Bitmap(_imageSize.Width, _imageSize.Height);
        _graphics = Graphics.FromImage(_bitmap);
        var font = new Font(FontFamily.GenericSansSerif, 20);
        
        _container = ContainerComposer.Compose(_center, _imageSize, _graphics, font,[]);
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
        
        expectedData.Should().BeEquivalentTo(resultData);
    }

    private TextRectangleContainer[] PrepareComposerExpectedData(string path)
    {
        var inputData = _container
            .Resolve<IFileReader>()
            .ReadFile(path);
        
        var data = _container
            .Resolve<BoringWordsProcessor>()
            .WordsToLowerAndRemoveBoringWords(inputData);
        
        var containers = new TextRectangleContainer[data.Count];
        var rect1 = new Rectangle(new Point(358, 383), new Size(84, 34));
        var rect2 = new Rectangle(new Point(404, 417), new Size(12, 17));
        containers[0] = new TextRectangleContainer(rect1, "слово", 1);
        containers[1] = new TextRectangleContainer(rect2, "в",0.5f );
        return containers;
    }

    [TearDown]
    public void TearDown()
    {
        _graphics.Dispose();
        _bitmap.Dispose();
    }
}