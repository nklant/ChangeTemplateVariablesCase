namespace RenameTemplateVariablesLowercase.Helpers;

public interface IHelpers
{
    string ProcessCase(string str);
    string ProcessCase(string[] parts);
    char ProcessCase(char ch);
}
