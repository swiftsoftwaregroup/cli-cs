using System.CommandLine;

var rootCommand = new RootCommand("CLI tool for greeting users in different languages");

var greetCommand = new Command("greet", "Greet a user based on a name in a file");
var fileArgument = new Argument<FileInfo>("file", "Path to the file containing the name");
greetCommand.AddArgument(fileArgument);

var languageOption = new Option<string>("--language", () => "en", "Language for the greeting (en: English, es: Spanish, bg: Bulgarian)");
languageOption.AddAlias("-l");
greetCommand.AddOption(languageOption);

greetCommand.SetHandler(async (file, language) =>
{
    var name = await CliCSharp.GreetingFunctions.ReadNameFromFileAsync(file.FullName);
    var greeting = CliCSharp.GreetingFunctions.GenerateGreeting(name, language);
    Console.WriteLine(greeting);
}, fileArgument, languageOption);

rootCommand.AddCommand(greetCommand);

return await rootCommand.InvokeAsync(args);



// top level statements must precede any namespace declaration
namespace CliCSharp
{
    public static class GreetingFunctions
    {
        public static async Task<string> ReadNameFromFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            return (await reader.ReadToEndAsync()).Trim();
        }

        public static string GenerateGreeting(string name, string language)
        {
            var greetings = new Dictionary<string, string>
        {
            { "en", $"Hello, {name}!" },
            { "es", $"¡Hola, {name}!" },
            { "bg", $"Здравей, {name}!" }
        };

            return greetings.TryGetValue(language, out var greeting)
                ? greeting
                : $"Unsupported language: {language}";
        }
    }
}