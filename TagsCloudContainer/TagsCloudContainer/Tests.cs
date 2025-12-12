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
    private Size _size;
    private IFileHandler _fileHandler;
    private string _filePath = "TestData";
    private IContainer _container;
    
    [SetUp]
    public void Setup()
    {
        _center = new Point(400, 400);
        _size = new Size(10, 20);
        _fileHandler = new TXTHndler();

        var builder = new ContainerBuilder();
        builder.RegisterInstance(new CircularCloudLayouter(_center)).As<ILayouter>();
        _container = builder.Build();
    }
    
    [Test]
    public void IFileHandler_ReadFile_Should_CorrectlyReadText()
    {
        var expectedData = new List<string> { "odin" };
        
        var data = _fileHandler.ReadFile(Path.Combine(_filePath,
            "IFileHandler_ReadFile_Should_CorrectlyReadText.txt"));
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void IFileHandler_WordsToLowerAndRemoveBoringWords_Should_CorrectlyProcessText()
    {
        var inputData = new List<string> { "скучное", "слово", "в", "СЛОВО" };
        var expectedData = new[] { "слово", "скучное" };
        
        var data = _fileHandler.WordsToLowerAndRemoveBoringWords(inputData);
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void IFIleHandler_HandleFile_Should_CorrectlyProcessText()
    {
        var expectedData = new[] { "слово", "скучное" };

        var data = _fileHandler.HandleFile(Path.Combine(_filePath,
            "IFIleHandler_HandleFile_Should_CorrectlyProcessText.txt"));
        
        data.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void DIContainer_Resolve_CircularCloudLayouter_Test()
    {
        var layouter = _container.Resolve<ILayouter>();
        layouter.Should().NotBeNull();
    }
}