﻿using System.Text;
using System.Text.RegularExpressions;
using ChangeTemplateVariablesCase.Helpers;
using Microsoft.Extensions.Configuration;

namespace ChangeTemplateVariablesCase;


public class App
{
    private readonly IConfiguration _config;
    private readonly IHelpers _helpers;

    public App(IConfiguration config, IHelpers helpers)
    {
        _config = config;
        _helpers = helpers;
    }

    /// <summary>
    /// A program to modify variables in html handlebars like this:
    /// {{ UPPER }} -> {{ upper }}
    /// {{ CamelCase }} -> {{ camelCase }}
    /// {{ Under_Line}} -> {{ under_Line }}
    /// {{NOSPACE}} -> {{nospace}}
    /// {{#if Variable }} -> {{#if variable }}
    /// {{#*inline "Variable"}}
    /// etc...
    /// </summary>
    public void Run(string[] args)
    {
        Console.WriteLine("Please enter a directory (or press Enter to use current):");
        string? directoryPath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                Console.WriteLine("Current directory is null!");
                return;
            }
        }

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory doesn't exist!");
            return;
        }

        string[] htmlFiles = GlobalConfig.AppsettingsFile && _config.GetValue<bool>("traverseSubDirs") ? 
                             Directory.GetFiles(directoryPath, "*.html", SearchOption.AllDirectories) : 
                             Directory.GetFiles(directoryPath, "*.html");

        foreach (string filePath in htmlFiles)
        {
            Console.WriteLine($"Processing file: {filePath}");

            int codepage = Encoding.UTF8.CodePage;
            Encoding encoding = Encoding.GetEncoding(codepage); // Determine encoding
            string htmlContent;

            try
            {
                htmlContent = File.ReadAllText(filePath, encoding);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read {filePath}: {ex.Message}");
                return;
            }

            string pattern1 = @"(?<=\{{2}\#\*\w+\s*"")\w+(?="")";
            string pattern2 = @"(?<=\{{2}\#?[\w\.]*\s*)[\w\.]+\s*(?=\}{2})";
            string pattern3 = @"(?<=\{\{>\s*)\w+(?=\}\})";
            string combinedPattern = pattern1 + "|" + pattern2 + "|" + pattern3;

            var regex = new Regex(combinedPattern);

            var matches = regex.Matches(htmlContent);

            // modify variables
            string newHtmlContent = regex.Replace(htmlContent, match =>
            {
                var oldVariable = match.Value.Trim();
                string newVariable;

                // if uppercase
                if (oldVariable == oldVariable.ToUpper())
                {
                    newVariable = _helpers.ProcessCase(oldVariable);
                }
                // if has underscore
                else if (oldVariable.Contains('_') && !oldVariable.Contains('.'))
                {
                    var parts = oldVariable.Split('_');
                    newVariable = _helpers.ProcessCase(parts);
                }
                // if has dot
                else if (oldVariable.Contains('.') || (oldVariable.Contains('.') && oldVariable.Contains('_')))
                {
                    var parts = oldVariable.Split('.');
                    newVariable = _helpers.ProcessCaseDot(parts);
                }
                // if CamelCase
                else
                {
                    newVariable = _helpers.ProcessCase(oldVariable[0]) + oldVariable.Substring(1);
                }

                // for visual debug and eye candy
                Console.WriteLine($"Changing {oldVariable} to {newVariable}");
                return newVariable;
            });

            string? directory = Path.GetDirectoryName(filePath);
            string filename = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            if (directory == null)
            {
                Console.WriteLine("Directory is null!");
                return;
            }

            string newFilePath = Path.Combine(directory, filename + "_modified" + extension);

            File.WriteAllText(newFilePath, newHtmlContent, Encoding.Unicode);
            Console.WriteLine($"Modified file: {newFilePath}" + Environment.NewLine);
        }

        // Some eye candy
        string plural = (htmlFiles.Length > 1 || htmlFiles.Length == 0) ? "s" : "";
        Console.WriteLine(htmlFiles.Length + " file" + plural + " processed." + Environment.NewLine);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
