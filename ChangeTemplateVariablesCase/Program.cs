using ChangeTemplateVariablesCase;
using ChangeTemplateVariablesCase.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    if (!File.Exists("appsettings.json"))
    {
        Console.WriteLine("appsettings.json doesn't exist! App is using default settings.");
        Console.WriteLine("You can create your own appsettings.json file to customize the settings with");
        Console.WriteLine("Example:");
        Console.WriteLine("\"firstLetterLowercase\" boolean value. (Should the app convert variables to start with uppercase)");
        Console.WriteLine("\"traverseSubDirs\" boolean value. (Should the app traverse sub-directories)" + Environment.NewLine);
        GlobalConfig.AppsettingsFile = false;
    }
    services.GetRequiredService<App>().Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

    IConfiguration config = builder.Build();

    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<App>();
            services.AddScoped<IHelpers, Helpers>();
        });
}
