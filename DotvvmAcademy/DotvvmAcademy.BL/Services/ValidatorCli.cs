using DotvvmAcademy.Validation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Services
{
    public class ValidatorCli
    {
        public const int ValidatorTimeout = 3000;

        public ValidatorCli(string cliDirectory)
        {
            CliDirectory = cliDirectory;
        }

        public string CliDirectory { get; }

        public async Task<IEnumerable<ValidationError>> Invoke(string codeLanguage, string validatorKey, IEnumerable<string> dependencies, string code)
        {
            var delayTaskCts = new CancellationTokenSource();
            var arguments = new List<string>
            {
                $@"{CliDirectory}\bin\Debug\netcoreapp2.0\DotvvmAcademy.Validation.Cli.dll",
                codeLanguage,
                validatorKey
            };
            if (dependencies.Count() > 0)
            {
                arguments.Add("-d");
                arguments.AddRange(dependencies.Select(d => $"\"{d}\""));
            }

            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                Arguments = string.Join(" ", arguments),
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = "dotnet"
            };
            process.EnableRaisingEvents = true;
            var tcs = new TaskCompletionSource<IEnumerable<ValidationError>>();
            process.Exited += async (sender, args) =>
            {
                if (!process.StandardError.EndOfStream)
                {
                    throw new ValidatorCliException(await process.StandardError.ReadToEndAsync());
                }

                var errors = new List<ValidationError>();
                while (!process.StandardOutput.EndOfStream)
                {
                    var line = await process.StandardOutput.ReadLineAsync();
                    errors.Add(ProcessCliLine(line));
                }
                tcs.SetResult(errors);
            };
            process.Start();
            await process.StandardInput.WriteAsync(code);
            process.StandardInput.Close();

            if (await Task.WhenAny(tcs.Task, Task.Delay(ValidatorTimeout, delayTaskCts.Token)) == tcs.Task)
            {
                // within timeout
                delayTaskCts.Cancel();
                return await tcs.Task;
            }
            else
            {
                // it takes too long
                process.Kill();
                var timeoutError = new ValidationError("The code timed out.");
                return new ValidationError[] { timeoutError };
            }
        }

        private ValidationError ProcessCliLine(string line)
        {
            var objectType = line.Substring(0, line.IndexOf(' '));
            if (objectType.Contains("Exception"))
            {
                throw new ValidatorCliException(line);
            }
            return ValidationError.FromString(line);
        }
    }
}