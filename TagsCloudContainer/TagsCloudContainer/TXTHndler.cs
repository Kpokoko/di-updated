using WeCantSpell.Hunspell;

namespace TagsCloudContainer;

internal class TXTHndler : IFileHandler
{
    private const float BoringWordQuantityThreshold = 0.35f;
    public Dictionary<string, int> HandleFile(string path, List<string> banWords)
    {
        var lines = ReadFile(path);
        return WordsToLowerAndRemoveBoringWords(lines, banWords);
    }
    
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

    public Dictionary<string, int> WordsToLowerAndRemoveBoringWords(List<string> words, List<string> banWords)
    {
        var banWordsDict = WordList.CreateFromWords(banWords);
        var frequencyDict = new Dictionary<string, int>();
        foreach (var word in words)
        {
            var lowerCaseWord = word.ToLower();
            if (banWordsDict.Check(lowerCaseWord))
                continue;
            frequencyDict.TryAdd(lowerCaseWord, 0);
            ++frequencyDict[lowerCaseWord];
            var a = (float)frequencyDict[lowerCaseWord] / words.Count;
        }
        
        return frequencyDict
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Where(x => ((float)x.Value / words.Count) < BoringWordQuantityThreshold)
            .ToDictionary(x => x.Key, x => x.Value);
    }
}