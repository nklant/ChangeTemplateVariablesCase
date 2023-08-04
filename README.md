# Change HTML Template Handlebars Variables To Lowercase/Uppercase
A use-case specific console appllication to modify the case of the variables in html handlebars from upper to lowercase and vice versa written in C# .NET 7

Examples:
{{ UPPER }} -> {{ upper }}
{{ CamelCase }} -> {{ camelCase }}
{{ Under_Line}} -> {{ under_Line }}
{{NOSPACE}} -> {{nospace}}
{{#if Variable }} -> {{#if variable }}
etc...

The application mode (Lowercase/Uppercase) is controlled via appsettings.json

Set the property "firstLetterLowercase": true/false

Default value: true

Input:

Directory of the html

/If none is given then the current dir is used/

Output:

The modified html with "_modified" at the end of the original file name.

<br /><br />
<a href="https://www.buymeacoffee.com/5hwn5ffjzsl" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>
