using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudApp;

public class ConsoleClient : IClient
{
    private const string ReadFontColorPrompt = "Введите английское название цвета шрифта (enter для дефолтного значения):";
    private const string ReadBGColorPrompt = "Введите английское название цвета фона (enter для дефолтного значения):";
    private const string ReadFontNamePrompt = "Введите название шрифта (enter для дефолтного значения):";
    private const string ReadFontSizePrompt = "Введите размер изображения через пробел двумя числами (enter для дефолтного значения):";
    private const string ReadOutputFileNamePrompt = "Введите желаемое имя для выходного файла (enter для дефолтного значения):";
    private const string ReadBanWordsPrompt = "Введите через запятую слова, которые хотите игнорировать (enter, если хотите видеть все слова)";
    private const string ReadOutputFileFormat = "Введите желаемый формат выходного файла (enter для дефолтного значения)";
    
    public ImageGeneratorInfo GetImageGeneratorInfo()
    {
        var textColor = ReadColor(ReadFontColorPrompt);
        var bgColor = ReadColor(ReadBGColorPrompt);
        var font = ReadFont(ReadFontNamePrompt);
        var imageSize = ReadSize(ReadFontSizePrompt);
        var outputFile = ReadString(ReadOutputFileNamePrompt);
        var banWords = ReadList(ReadBanWordsPrompt);
        var outputFileFormat = ReadFormat(ReadOutputFileFormat);

        return new ImageGeneratorInfo(
            textColor,
            bgColor,
            font,
            imageSize,
            outputFile,
            banWords,
            outputFileFormat
        );
    }

    private Color? ReadColor(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;
            var color = Color.FromName(input);
            if (color.IsKnownColor)
                return color;
            Console.WriteLine("Такого цвета нет, попробуй ещё раз");
        }
    }

    private Font? ReadFont(string prompt, float defaultSize = 60)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;
            if (FontFamily.Families.Any(f => f.Name.Equals(input, StringComparison.OrdinalIgnoreCase)))
                return new Font(input, defaultSize);
            Console.WriteLine("Такого шрифта нет, попробуй ещё раз");
        }
    }

    private Size? ReadSize(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out var width) &&
                int.TryParse(parts[1], out var height))
            {
                return new Size(width, height);
            }
            Console.WriteLine("Нужно ввести два числа через пробел");

        }
    }

    private string? ReadString(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? null : input;
    }

    private List<string>? ReadList(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? new List<string>() : input.Split(',').Select(s => s.Trim()).ToList();
    }

    private string? ReadFormat(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return null;
        var isCorrectFileFormatInputed = false;
        while (!isCorrectFileFormatInputed)
        {
            if (input[0] == '.')
                input = input[1..];
            switch (input)
            {
                case "png":
                    isCorrectFileFormatInputed = true;
                    break;
                case "jpg":
                    isCorrectFileFormatInputed = true;
                    break;
                case "jpeg":
                    isCorrectFileFormatInputed = true;
                    break;
                case "bmp":
                    isCorrectFileFormatInputed = true;
                    break;
                default:
                    Console.WriteLine("Неверный формат, повторите попытку!");
                    input = Console.ReadLine();
                    break;
            }
        }
        return input;
    }

    public string GetImagePath()
    {
        Console.WriteLine("Введи название входного файла:");
        var fileName = Console.ReadLine();
        while (!File.Exists(fileName))
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Имя файла не может быть пустым!");
                fileName = Console.ReadLine();
                continue;
            }
            var fullPath = Path.GetFullPath(fileName);
            Console.WriteLine($"Файл {fileName} не найден по пути {fullPath}, повтори ввод");
            fileName = Console.ReadLine();
        }
        return fileName;
    }
}
