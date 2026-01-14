namespace TagsCloudContainer;

public interface IFileHandler
{
    internal Dictionary<string, int> HandleFile(string path, List<string> banWords);
    internal List<string> ReadFile(string path);
    
    internal Dictionary<string, int> WordsToLowerAndRemoveBoringWords(List<string> words, List<string> banWords);
}