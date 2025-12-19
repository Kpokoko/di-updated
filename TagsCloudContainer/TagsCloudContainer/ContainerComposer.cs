using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public static class ContainerComposer
{
    public static IContainer Compose(Point center, Size firstRectSize)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(new CircularCloudLayouter(center)).As<ILayouter>();
        builder.RegisterInstance(new TXTHndler()).As<IFileHandler>();
        builder.RegisterInstance(new RectangleSizeCalculator(firstRectSize));
        builder.RegisterType<TextRectangleContainerProcessor>();
        return builder.Build();
    }
}