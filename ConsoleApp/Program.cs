﻿using ConsoleApp.Configuration.Models;
using Microsoft.Extensions.Configuration;


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