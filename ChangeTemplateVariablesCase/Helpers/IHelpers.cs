﻿namespace ChangeTemplateVariablesCase.Helpers;

public interface IHelpers
{
    string ProcessCase(string str);
    string ProcessCase(string[] parts);
    string ProcessCaseDot(string[] parts);
    char ProcessCase(char ch);
}
