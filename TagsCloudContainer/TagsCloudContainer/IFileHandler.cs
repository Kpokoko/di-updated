namespace TagsCloudContainer;

public interface IFileHandler
{
    public string[] HandleFile(string path);
    internal List<string> ReadFile(string path);
    
    internal string[] WordsToLowerAndRemoveBoringWords(List<string> words);
}