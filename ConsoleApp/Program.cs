using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    //package Microsoft.Extensions.Configuration.FileExtensions
    //package Microsoft.Extensions.Configuration.Json
    .AddJsonFile("Configuration\\config.json", optional: false, reloadOnChange: true)
    //package Microsoft.Extensions.Configuration.Xml
    .AddXmlFile("Configuration\\configx.xml", optional: true)
    //package Microsoft.Extensions.Configuration.Ini
    .AddIniFile("Configuration\\config.ini")
    //package NetEscapades.Configuration.Yaml
    .AddYamlFile("Configuration\\config.yaml")
    .Build();

for (int i = 0, limit = int.Parse(config["Repeat"]); i < limit; i++)
{
    Console.WriteLine($"Hello from {config["HelloJson"]}");
    Console.WriteLine($"Hello from {config["HelloXml"]}");
    Console.WriteLine($"Hello from {config["HelloIni"]}");
    Console.WriteLine($"Hello from {config["HelloYaml"]}");

    Console.WriteLine($"{config["Bye"]}");

    Thread.Sleep(1000);
}