using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudContainer.Tests;

[TestFixture]
public class DIContainerTests
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
    public void DIContainer_Resolve_CircularCloudLayouter_Test()
    {
        var layouter = _container.Resolve<ILayouter>();
        layouter.Should().NotBeNull();
    }

    [TearDown]
    public void TearDown()
    {
        _graphics.Dispose();
        _bitmap.Dispose();
    }
}