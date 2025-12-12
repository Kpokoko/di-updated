using System.Drawing;

namespace TagsCloudContainer;

public interface IImageGenerator
{
    public void GenerateImage(ImageGeneratorInfo imageGeneratorInfo);
}