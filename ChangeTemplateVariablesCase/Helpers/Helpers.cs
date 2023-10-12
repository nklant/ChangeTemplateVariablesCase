using Microsoft.Extensions.Configuration;
using System.Text;

namespace ChangeTemplateVariablesCase.Helpers;

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
        StringBuilder result = new StringBuilder();

        bool firstLetterLowercase = _config.GetValue<bool>("firstLetterLowercase");

        for (int i = 0; i < parts.Length; i++)
        {
            if (firstLetterLowercase || !GlobalConfig.AppsettingsFile)
            {
                result.Append(Char.ToLower(parts[i][0]) + parts[i].Substring(1));
            }
            else
            {
                result.Append(Char.ToUpper(parts[i][0]) + parts[i].Substring(1));
            }

            if (i < parts.Length - 1)
            {
                result.Append('_');
            }
        }

        return result.ToString();
    }

    public string ProcessCaseDot(string[] parts)
    {
        StringBuilder result = new StringBuilder();

        bool firstLetterLowercase = _config.GetValue<bool>("firstLetterLowercase");

        for (int i = 0; i < parts.Length; i++)
        {
            // Only change the first letter of the part before the underscore
            if (firstLetterLowercase || !GlobalConfig.AppsettingsFile)
            {
                int underscoreIndex = parts[i].IndexOf('_');
                if (underscoreIndex == -1)
                {
                    // If there's no underscore, process the whole part
                    result.Append(Char.ToLower(parts[i][0]) + parts[i].Substring(1));
                }
                else
                {
                    // Process only up to the underscore, then append the rest unchanged
                    result.Append(Char.ToLower(parts[i][0]) + parts[i].Substring(1, underscoreIndex) + parts[i].Substring(underscoreIndex));
                }
            }
            else
            {
                result.Append(Char.ToUpper(parts[i][0]) + parts[i].Substring(1));
            }

            if (i < parts.Length - 1)
            {
                result.Append('.');
            }
        }

        return result.ToString();
    }

    public char ProcessCase(char ch)
    {
        if (_config.GetValue<bool>("firstLetterLowercase"))
        {
            return Char.ToLower(ch);
        }
        else if (_config.GetValue<bool>("firstLetterLowercase") == false && GlobalConfig.AppsettingsFile)
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
