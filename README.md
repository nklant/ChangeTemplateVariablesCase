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



<a href="https://bmc.link/5hwn5ffjzsl" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>
