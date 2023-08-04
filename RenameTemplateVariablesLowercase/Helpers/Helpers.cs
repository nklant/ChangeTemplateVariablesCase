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
        if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return str.ToLower();
        }
        else if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return str.ToUpper();
        }
        else
        {
            throw new Exception("Cannot lookup appsettings!");
        }
    }

    public string ProcessCase(string[] parts)
    {
        if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return Char.ToLower(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
        }
        else if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return Char.ToUpper(parts[0][0]) + parts[0].Substring(1) + "_" + parts[1];
        }
        else
        {
            throw new Exception("Cannot lookup appsettings!");
        }
    }

    public char ProcessCase(char ch)
    {
        if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return Char.ToLower(ch);
        }
        else if (_config.GetValue<bool>("FirstLetterLowercase") == true)
        {
            return Char.ToUpper(ch);
        }
        else
        {
            throw new Exception("Cannot lookup appsettings!");
        }
    }
}
