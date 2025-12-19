using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudApp;

public class ConsoleClient : IClient
{
    public ImageGeneratorInfo GetImageGeneratorInfo()
    {
        var textColor = ReadColor("Введите английское название цвета шрифта (enter для дефолтного значения):");
        var bgColor = ReadColor("Введите английское название цвета фона (enter для дефолтного значения):");
        var font = ReadFont("Введите название шрифта (enter для дефолтного значения):");
        var imageSize = ReadSize("Введите размер изображения через пробел двумя числами (enter для дефолтного значения):");
        var outputFile = ReadString("Введите желаемое имя для выходного файла (enter для дефолтного значения):");

        return new ImageGeneratorInfo(
            textColor,
            bgColor,
            font,
            imageSize,
            outputFile
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
            Console.WriteLine("Это не цвет, бро");
        }
    }

    private Font? ReadFont(string prompt, float defaultSize = 20)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;
            if (FontFamily.Families.Any(f => f.Name.Equals(input, StringComparison.OrdinalIgnoreCase)))
                return new Font(input, defaultSize);
            Console.WriteLine("Такого шрифта нет, бро. Попробуй ещё раз.");
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

            Console.WriteLine("Введи два числа через пробел, бро");
        }
    }

    private string? ReadString(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? null : input;
    }

    public string GetImagePath()
    {
        Console.WriteLine("Введите название входного файла:");
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
            Console.WriteLine($"Этот файл не найден по пути {fullPath}, повторите ввод");
            fileName = Console.ReadLine();
        }
        return fileName;
    }
}
