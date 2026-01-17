using WeCantSpell.Hunspell;

namespace TagsCloudContainer;

public class BoringWordsProcessor
{
    private const float BoringWordQuantityThreshold = 0.35f;
    private readonly List<string> _banWords;

    public BoringWordsProcessor(List<string> banWords)
    {
        this._banWords = banWords;
    }
    
    public Dictionary<string, int> WordsToLowerAndRemoveBoringWords(List<string> words)
    {
        var banWordsDict = WordList.CreateFromWords(_banWords);
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