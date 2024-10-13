using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using CliCSharp;

namespace CliCSharp.Tests
{
    public class GreetingFunctionsTests
    {
        [Fact]
        public async Task ReadNameFromFileAsync_ShouldReturnTrimmedName()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFile, "  John Doe  ");

            try
            {
                // Act
                var result = await GreetingFunctions.ReadNameFromFileAsync(tempFile);

                // Assert
                Assert.Equal("John Doe", result);
            }
            finally
            {
                // Clean up
                File.Delete(tempFile);
            }
        }

        [Theory]
        [InlineData("John", "en", "Hello, John!")]
        [InlineData("Maria", "es", "¡Hola, Maria!")]
        [InlineData("Ivan", "bg", "Здравей, Ivan!")]
        [InlineData("Alice", "fr", "Unsupported language: fr")]
        public void GenerateGreeting_ShouldReturnCorrectGreeting(string name, string language, string expected)
        {
            // Act
            var result = GreetingFunctions.GenerateGreeting(name, language);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}