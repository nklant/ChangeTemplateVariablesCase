# Change HTML Template Handlebars Variables To Lowercase/Uppercase
A use-case specific console appllication to modify the case of the variables in html handlebars, starting with upper to lowercase and vice versa written in C# .NET 7

Examples: <br />
{{ UPPER }} -> {{ upper }}<br />
{{ CamelCase }} -> {{ camelCase }}<br />
{{ Under_Line}} -> {{ under_Line }}<br />
{{NOSPACE}} -> {{nospace}}<br />
{{#if Variable }} -> {{#if variable }}<br />
{{#*inline "Variable"}} -> {{#*inline "variable"}}<br />
etc...<br />

The application mode (Lowercase/Uppercase) is controlled via appsettings.json

Set the property "firstLetterLowercase": true/false

Default value: true

Input:

Directory of the html

/If none is given then the current dir is used/ <br />
/The app traverses all sub-directories/

Output:

The modified html with "_modified" at the end of the original file name.

<br /><br />
<a href="https://www.buymeacoffee.com/5hwn5ffjzsl" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>

<br /><br />
<img src="https://media.npr.org/assets/img/2023/05/26/honest-work-meme-cb0f0fb2227fb84b77b3c9a851ac09b095ab74d8-s1100-c50.jpg" 
     width="408" 
     height="306" />
