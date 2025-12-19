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
        var firstRectSize = new Size(30, 20);
        var container = ContainerComposer.Compose(imageCenter, firstRectSize);
        
        var wordsContainers = container
            .Resolve<TextRectangleContainerProcessor>()
            .ProcessFile(fileName);
        
        CloudVisualizer.Draw(wordsContainers, imageGeneratorInfo);
    }
}