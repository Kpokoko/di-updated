using System.Drawing;
using NUnit.Framework;
using Autofac;
using FluentAssertions;

namespace TagsCloudContainer.Tests;

[TestFixture]
public class BoringWordsProcessorTests
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
        
        _container = ContainerComposer.Compose(_center, _imageSize, _graphics, font,["банворд"]);
    }

    [Test]
    public void WordsToLowerAndRemoveBoringWords_HandleFile_Should_IgnoreBanWords()
    {
        var path = Path.Combine(_filePath,
            "TXTReader_ReadFile_Should_IgnoreBanWords.txt");
        var expectedData = new[] { "слово" };
        
        var inputData = _container
            .Resolve<IFileReader>()
            .ReadFile(path);
        
        var resultData = _container
            .Resolve<BoringWordsProcessor>()
            .WordsToLowerAndRemoveBoringWords(inputData).Keys;
        
        resultData.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void WordsToLowerAndRemoveBoringWords_HandleFile_Should_MakeWordsToLower()
    {
        var path = Path.Combine(_filePath,
            "WordsToLowerAndRemoveBoringWords_HandleFile_Should_MakeWordsToLower.txt");
        var expectedData = new[] { "слово" };
        
        var inputData = _container
            .Resolve<IFileReader>()
            .ReadFile(path);
        
        var resultData = _container
            .Resolve<BoringWordsProcessor>()
            .WordsToLowerAndRemoveBoringWords(inputData).Keys;
        
        resultData.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void WordsToLowerAndRemoveBoringWords_HandleFile_Should_IgnoreBoringWords()
    {
        var path = Path.Combine(_filePath,
            "WordsToLowerAndRemoveBoringWords_HandleFile_Should_IgnoreBoringWords.txt");
        var expectedData = new string[0];
        
        var inputData = _container
            .Resolve<IFileReader>()
            .ReadFile(path);
        
        var resultData = _container
            .Resolve<BoringWordsProcessor>()
            .WordsToLowerAndRemoveBoringWords(inputData).Keys;
        
        resultData.Should().BeEquivalentTo(expectedData);
    }

    [TearDown]
    public void TearDown()
    {
        _graphics.Dispose();
        _bitmap.Dispose();
    }
}