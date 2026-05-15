using PageEngine.Generation;
using PageEngine.Models;

namespace PageEngine;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PageEngine --input <content-dir> --output <output-dir>");
            return 1;
        }

        string? inputDir = null;
        string? outputDir = null;

        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--input")  inputDir  = args[i + 1];
            if (args[i] == "--output") outputDir = args[i + 1];
        }

        if (inputDir is null || outputDir is null)
        {
            Console.Error.WriteLine("Both --input and --output are required.");
            return 1;
        }

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return 1;
        }

        var configPath = Path.Combine(inputDir, "site.json");
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"site.json not found in input directory: {inputDir}");
            return 1;
        }

        var config = JsonSerializer.Deserialize<SiteConfig>(
            File.ReadAllText(configPath),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;

        var templateDir = Path.Combine(inputDir, "templates");

        var generator = new SiteGenerator(config, inputDir, templateDir, outputDir);
        generator.Generate();

        Console.WriteLine($"Site generated → {outputDir}");
        return 0;
    }
}
