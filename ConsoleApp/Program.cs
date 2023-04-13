using ConsoleApp;
using ConsoleApp.Configuration.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Authentication.ExtendedProtection;

var variable = Environment.GetEnvironmentVariable("enviroment_type");

var config = new ConfigurationBuilder()
    //package Microsoft.Extensions.Configuration.FileExtensions
    //package Microsoft.Extensions.Configuration.Json
    .AddJsonFile("Configuration\\config.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"Configuration\\config.{variable}.json", optional: true)
    //package Microsoft.Extensions.Configurat  ion.Xml
    .AddXmlFile("Configuration\\configx.xml", optional: true)
    //package Microsoft.Extensions.Configuration.Ini
    .AddIniFile("Configuration\\config.ini")
    //package NetEscapades.Configuration.Yaml
    .AddYamlFile("Configuration\\config.yaml")
    //package Microsoft.Extensions.Configuration.EnvironmentVariables
    .AddEnvironmentVariables()
    .Build();

//Microsoft.Extensions.DependencyInjection
IServiceCollection serviceCollection = new ServiceCollection();
//konfiguracja usług
serviceCollection.AddTransient<ConsoleOutputService>();
serviceCollection.AddTransient<IOutputService, ConsoleOutputService>();
serviceCollection.AddTransient<IOutputService, DebugOuputService>();

//Singleton - zawsze ta sama instancja - tylko raz wywoływany konstruktor danej klasy
serviceCollection.AddSingleton<IFontService, StandardFontService>();
serviceCollection.AddSingleton<IFontService, SweetFontService>();

//Scoped - instancja tworzona dla każdego nowego scope
serviceCollection.AddScoped<IFontService, SubZeroFontService>();
serviceCollection.AddScoped<IFontService, TengwarFontService>();

//Transient - zawsze nowa instancja
serviceCollection.AddTransient<IOutputService, RandomFontConsoleOutputService>();

serviceCollection.AddSingleton<AppConfig>(x =>
{
    var appConfig = new AppConfig();
    config.Bind(appConfig);
    return appConfig;
});

serviceCollection.AddLogging(options => options
                                            .AddConfiguration(config.GetSection("Logging"))
                                            //.SetMinimumLevel(LogLevel.Trace)
                                            .AddConsole(/*x => x.IncludeScopes = true*/)
                                            .AddDebug()
                                            .AddEventLog());
serviceCollection.AddSingleton<LoggerDemo>();


IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();


serviceProvider.GetService<LoggerDemo>().Work();





static void ConfigurationDemo(IConfigurationRoot config)
{
    //for (int i = 0, limit = int.Parse(config["Repeat"]); i < limit; i++)
    {
        Console.WriteLine($"Hello from {config["HelloJson"]}");
        Console.WriteLine($"Hello from {config["HelloXml"]}");
        Console.WriteLine($"Hello from {config["HelloIni"]}");
        Console.WriteLine($"Hello from {config["HelloYaml"]}");

        Console.WriteLine($"{config["Bye"]}");

        Thread.Sleep(1000);
    }

    Console.WriteLine($"{config["Greetings:Greeting1"]} from {config["Greetings:Targets:AI"]}");

    var greetingsSection = config.GetSection("Greetings");
    var targetsSection = greetingsSection.GetSection("Targets");
    //var targetsSection = config.GetSection("Greetings:Targets");

    Console.WriteLine($"{greetingsSection["Greeting1"]} from {targetsSection["AI"]}");

    var connectionString = config.GetConnectionString("myDB");
    Console.WriteLine(connectionString);


    AppConfig appConfig = new();
    //Microsoft.Extensions.Configuration.Binder
    config.Bind(appConfig);


    //for (int i = 0, limit = appConfig.Repeat; i < limit; i++)
    for (int i = 0, limit = config.GetValue<int>("Repeat"); i < limit; i++)
        Console.WriteLine($"{appConfig.Greetings.Greeting1} from {appConfig.Greetings.Targets.AI}");


    Console.WriteLine(config["enviroment_type"]);
}

static void DiDemo(IServiceProvider serviceProvider)
{
    //serviceProvider.GetService<IOutputService>().WriteLine("Ala ma kota");

    //foreach (var item in serviceProvider.GetServices<IOutputService>())
    var outputServices = serviceProvider.GetServices<IOutputService>().ToList();
    for (int i = 0; i < outputServices.Count(); i++)
    {
        outputServices[i].WriteLine($"Ala ma kota: {i}");
    }


    IServiceScope scope = null;
    for (int i = 0; i < 10; i++)
    {
        if (i % 3 == 0)
        {
            scope?.Dispose();
            scope = serviceProvider.CreateScope();
        }

        scope.ServiceProvider.GetService<IOutputService>().WriteLine("Hello!");
    }
    scope.Dispose();
}