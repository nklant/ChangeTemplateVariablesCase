using Microsoft.Extensions.Configuration;

namespace RenameTemplateVariablesLowercase.Helpers;

public class Helpers : IHelpers
{
    private readonly IConfiguration _config;

    public Helpers(IConfiguration config)
    {
        _config = config;
    }

    public string ProcessCase(string str)
    {
        if (_config.GetValue<bool>("firstLetterLowercase"))
        {
            return str.ToLower();
        }
        else if (_config.GetValue<bool>("firstLetterLowercase") == false && GlobalConfig.AppsettingsFile)
        {
            return str.ToUpper();
        }
        else
        {
            // If appsettings.json doesn't exist, then default to lowercase
            return str.ToLower();
        }
    }

    public string ProcessCase(string[] parts)
    {
        if (_config.GetValue<bool>("firstLetterLowercase"))
        {
            return Char.ToLower(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
        }
        else if (_config.GetValue<bool>("firstLetterLowercase") == false && GlobalConfig.AppsettingsFile)
        {
            return Char.ToUpper(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
        }
        else
        {
            // If appsettings.json doesn't exist, then default to lowercase
            return Char.ToLower(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
        }
    }

    public char ProcessCase(char ch)
    {
        if (_config.GetValue<bool>("firstLetterLowercase"))
        {
            return Char.ToLower(ch);
        }
        else if (_config.GetValue<bool>("firstLetterLowercase") == false && File.Exists("appsettings.json"))
        {
            return Char.ToUpper(ch);
        }
        else
        {
            // If appsettings.json doesn't exist, then default to lowercase
            return Char.ToLower(ch);
        }
    }
}
