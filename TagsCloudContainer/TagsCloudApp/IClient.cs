using TagsCloudContainer;

namespace TagsCloudApp;

public interface IClient
{
    public ImageGeneratorInfo GetImageGeneratorInfo();

    public string GetImagePath();
}