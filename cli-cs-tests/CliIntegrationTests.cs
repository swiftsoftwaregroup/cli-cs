using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CliCSharp.Tests
{
    public class CliIntegrationTests
    {
        [Fact]
        public async Task CliApplication_ShouldGreetCorrectly()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFile, "John Doe");

            try
            {
                // Act
                var result = await RunCliCommand($"greet {tempFile}");

                // Assert
                Assert.Contains("Hello, John Doe!", result);

                // Act
                result = await RunCliCommand($"greet -l es {tempFile}");

                // Assert
                Assert.Contains("¡Hola, John Doe!", result);
            }
            finally
            {
                // Clean up
                File.Delete(tempFile);
            }
        }

        private async Task<string> RunCliCommand(string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"run --project ../../../../cli-cs -- {arguments}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return output;
        }
    }
}