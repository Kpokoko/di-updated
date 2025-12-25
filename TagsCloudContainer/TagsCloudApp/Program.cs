using System.Drawing;
using Autofac;
using TagsCloudApp;
using TagsCloudContainer;

public static class Program
{
    public static void Main()
    {
        var client = new ConsoleClient();
        var imageGeneratorInfo = client.GetImageGeneratorInfo();
        var fileName = client.GetImagePath();
        var imageSize = imageGeneratorInfo.ImageSize;
        var imageCenter = new Point(imageSize.Width / 2, imageSize.Height / 2);
        CloudVisualizer.PrepareGraphics(imageSize);
        var container = ContainerComposer.Compose(imageCenter, imageGeneratorInfo.ImageSize,
            CloudVisualizer.Graphics, imageGeneratorInfo.Font);
        
        var wordProcessor = container
            .Resolve<TextRectangleContainerProcessor>();
        
        var measurer = container
            .Resolve<IWordMeasurer>();
        
        CloudVisualizer.Draw(wordProcessor, imageGeneratorInfo, fileName, measurer);
    }
}