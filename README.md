# Rename Template Variables To Lowercase/Uppercase
A console appllicatoin to modify first character of the variables in html handlebars from upper to lowercase and vice versa.

A program to modify variables in html handlebars like this:
{{ UPPER }} -> {{ upper }}
{{ CamelCase }} -> {{ camelCase }}
{{ Under_Line}} -> {{ under_Line }}
{{NOSPACE}} -> {{nospace}}
{{#if Variable }} -> {{#if variable }}
etc...

The application mode is controlled via appsettings.json

Set the property "firstLetterLowercase": true/false

Default value: true

Input:

Directory of the html

/If none is given then the current dir is used/

Output:

The modified html with "_modified" at the end of the original file name.
