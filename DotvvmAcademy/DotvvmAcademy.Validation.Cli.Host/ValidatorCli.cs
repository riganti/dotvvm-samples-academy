using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Cli.Host
{
    public class ValidatorCli
    {
        private ValidatorCliLineProcessor lineProcessor = new ValidatorCliLineProcessor();

        public const string CliAssemblyFileName = "DotvvmAcademy.Validation.Cli.dll";

        public ValidatorCli()
        {
            CliPath = GetCliPath();
        }

        public string CliPath { get; }

        public ValidatorTimeout Timeout { get; private set; }

        public async Task<IEnumerable<ValidationError>> Invoke(ValidatorCliArguments arguments, string code)
        {
            var completionSource = new TaskCompletionSource<IEnumerable<ValidationError>>();
            var process = new Process();
            process.StartInfo = GetProcessStartInfo(arguments);
            process.EnableRaisingEvents = true;
            process.Exited += async (sender, args) =>
            {
                await ProcessStandardError(process.StandardError);
                completionSource.SetResult(await ProcessValidationErrors(process.StandardOutput));
            };
            process.Start();

            Timeout = await ProcessTimeout(process.StandardOutput);
            await process.StandardInput.WriteAsync(code);
            process.StandardInput.Close();

            var tokenSource = new CancellationTokenSource();
            if (await Task.WhenAny(completionSource.Task, Task.Delay(Timeout.Timeout, tokenSource.Token)) == completionSource.Task)
            {
                // within timeout
                tokenSource.Cancel();
                return await completionSource.Task;
            }
            else
            {
                // after timeout has passed
                process.Kill();
                var timeoutError = new ValidationError("The code timed out.");
                return new ValidationError[] { timeoutError };
            }
        }

        private async Task ProcessStandardError(StreamReader standardError)
        {
            if (!standardError.EndOfStream)
            {
                throw new ValidatorCliException(await standardError.ReadToEndAsync());
            }
        }

        private ProcessStartInfo GetProcessStartInfo(ValidatorCliArguments arguments)
        {
            arguments.ValidatorCliPath = CliPath;
            return new ProcessStartInfo()
            {
                Arguments = arguments.ToString(),
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = "dotnet"
            };
        }

        private async Task<IEnumerable<ValidationError>> ProcessValidationErrors(StreamReader standardOutput)
        {
            var errors = new List<ValidationError>();
            while (!standardOutput.EndOfStream)
            {
                var lineObject = lineProcessor.ProcessLine(await standardOutput.ReadLineAsync());
                if(lineObject is Exception exception)
                {
                    throw exception;
                }
                else if(lineObject is ValidationError error)
                {
                    errors.Add(error);
                }
            }
            return errors;
        }

        private async Task<ValidatorTimeout> ProcessTimeout(StreamReader standardOutput)
        {
            return lineProcessor.ProcessLine<ValidatorTimeout>(await standardOutput.ReadLineAsync());
        }

        private string GetCliPath()
        {
            var cliHostPath = typeof(ValidatorCli).Assembly.Location;
            var parentDirectory = Directory.GetParent(cliHostPath);
            var cliPath = Path.Combine(parentDirectory.FullName, $"./ValidatorCli/{CliAssemblyFileName}");
            var exists = File.Exists(cliPath);
            if(exists)
            {
                return cliPath;
            }
            else
            {
                throw new ValidatorCliException($"The Cli assembly doesn't exists at '{cliPath}'.");
            }
        }
    }
}