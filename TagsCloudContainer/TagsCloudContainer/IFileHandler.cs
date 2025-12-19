namespace TagsCloudContainer;

public interface IFileHandler
{
    internal Dictionary<string, int> HandleFile(string path);
    internal List<string> ReadFile(string path);
    
    internal Dictionary<string, int> WordsToLowerAndRemoveBoringWords(List<string> words);
}