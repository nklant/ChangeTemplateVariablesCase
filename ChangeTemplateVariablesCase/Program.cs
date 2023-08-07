using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RenameTemplateVariablesLowercase;
using RenameTemplateVariablesLowercase.Helpers;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    if (!File.Exists("appsettings.json"))
    {
        Console.WriteLine("appsettings.json doesn't exist! Using default settings.");
        Console.WriteLine("You can create your own appsettings.json file to customize the settings with a \"firstLetterLowercase\" boolean value.");
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
