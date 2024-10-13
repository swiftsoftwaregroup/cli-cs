using System.CommandLine;

/// <summary>
/// CLI tool for greeting users in different languages.
/// </summary>
/// <remarks>
/// This application reads a name from a file and generates a greeting in a specified language.
/// It supports English (default), Spanish, and Bulgarian greetings.
/// </remarks>

// Main program logic
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
    /// <summary>
    /// Provides functions for reading names from files and generating greetings in different languages.
    /// </summary>
    public static class GreetingFunctions
    {
        /// <summary>
        /// Asynchronously reads a name from a file.
        /// </summary>
        /// <param name="filePath">The path to the file containing the name.</param>
        /// <returns>A task that represents the asynchronous read operation. The task result contains the name as a string, trimmed of leading and trailing whitespace.</returns>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file is not found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading the file.</exception>
        public static async Task<string> ReadNameFromFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            return (await reader.ReadToEndAsync()).Trim();
        }

        /// <summary>
        /// Generates a greeting for a given name in the specified language.
        /// </summary>
        /// <param name="name">The name to include in the greeting.</param>
        /// <param name="language">The language code for the greeting. Supported languages are "en" (English), "es" (Spanish), and "bg" (Bulgarian).</param>
        /// <returns>A greeting string in the specified language.</returns>
        /// <remarks>
        /// If an unsupported language code is provided, the method returns an "Unsupported language" message.
        /// </remarks>
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