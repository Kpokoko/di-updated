using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public static class ContainerComposer
{
    public static IContainer Compose(Point center, Size imageSize, Graphics graphics, Font font, List<string> banWords)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(new CircularCloudLayouter(center)).As<ILayouter>();
        builder.RegisterInstance(new TxtReader()).As<IFileReader>();
        builder.RegisterInstance(new RectangleSizeCalculator(imageSize));
        builder.RegisterInstance(new BoringWordsProcessor(banWords));
        builder.RegisterType<TextRectangleContainerProcessor>();
        builder.RegisterInstance(new WordMeasurer(graphics, font)).As<IWordMeasurer>();
        return builder.Build();
    }
}