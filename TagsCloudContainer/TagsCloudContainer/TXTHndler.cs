namespace TagsCloudContainer;

public class TXTHndler : IFileHandler
{
    public string[] HandleFile(string path)
    {
        var lines = ReadFile(path);
        return WordsToLowerAndRemoveBoringWords(lines);
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

    public string[] WordsToLowerAndRemoveBoringWords(List<string> words)
    {
        var frequencyDict = new Dictionary<string, int>();
        foreach (var word in words)
        {
            var lowerCaseWord = word.ToLower();
            if (!frequencyDict.ContainsKey(lowerCaseWord))
                frequencyDict[lowerCaseWord] = 0;
            ++frequencyDict[lowerCaseWord];
        }
        // TODO фильтрация "скучных" слов
        // Пока что ничего лучше подключения либы для определения частей слов, как просят в доп заданиях, не придумал
        // Хороший ли это вариант, или стоит подумать ещё?
        return frequencyDict.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
    }
}