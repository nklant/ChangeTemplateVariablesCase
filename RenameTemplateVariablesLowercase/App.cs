using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace RenameTemplateVariablesLowercase;

public class App
{
    private readonly IConfiguration _config;

    public App(IConfiguration config)
    {
        _config = config;
    }

    public void Run(string[] args)
    {
        // Should the fist letter be lowercase/uppercase?
        if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            /// TODO: Make .ToLower() / .ToUpper() extension method toggling between the two
        }

        Console.WriteLine("Please enter a directory (or press Enter to use current):");
        string directoryPath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory doesn't exist!");
            return;
        }

        string[] htmlFiles = Directory.GetFiles(directoryPath, "*.html");

        foreach (string filePath in htmlFiles)
        {
            Console.WriteLine($"Processing file: {filePath}");
            string htmlContent = File.ReadAllText(filePath, Encoding.Unicode);

            // FUCKING REGEX SPACE ALIEN LANGUAGE I FUCKING HATE YOU!!!!!
            string pattern = @"(?<=\{{2}\#?\w*\s*)\w+\s*(?=\}{2})";
            var regex = new Regex(pattern);

            var matches = regex.Matches(htmlContent);

            // modify variables
            string newHtmlContent = regex.Replace(htmlContent, match =>
            {
                var oldVariable = match.Value.Trim();
                string newVariable;

                // if uppercase
                if (oldVariable == oldVariable.ToUpper())
                {
                    newVariable = oldVariable.ToLower();
                }
                // if has underscore
                else if (oldVariable.Contains("_"))
                {
                    var parts = oldVariable.Split('_');
                    newVariable = Char.ToLower(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
                }
                // if CamelCase
                else
                {
                    newVariable = Char.ToLower(oldVariable[0]) + oldVariable.Substring(1);
                }

                // for visual debug and eye candy
                Console.WriteLine($"Changing {oldVariable} to {newVariable}");
                return newVariable;
            });

            string directory = Path.GetDirectoryName(filePath);
            string filename = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            string newFilePath = Path.Combine(directory, filename + "_modified" + extension);

            File.WriteAllText(newFilePath, newHtmlContent, Encoding.Unicode);
            Console.WriteLine($"Modified file: {newFilePath}");
        }

        Console.WriteLine("Press any key... Where is 'any key' ??? :D");
        Console.ReadKey();
    }
}
