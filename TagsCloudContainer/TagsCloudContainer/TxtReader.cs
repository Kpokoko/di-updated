using WeCantSpell.Hunspell;

namespace TagsCloudContainer;

internal class TxtReader : IFileReader
{
    public List<string> ReadFile(string path)
    {
        var parsedWords = new List<string>();
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line is null)
                    continue;
                parsedWords.Add(line);
            }
        }
        return parsedWords;
    }
}