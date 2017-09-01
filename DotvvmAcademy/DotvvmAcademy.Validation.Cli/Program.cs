using Microsoft.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Reflection;

namespace DotvvmAcademy.Validation.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cla = new CommandLineApplication();
            ConfigureCommandLineApplication(cla);
            cla.Execute(args);
        }

        private static void ConfigureCommandLineApplication(CommandLineApplication cla)
        {
            cla.HelpOption("-h|--help|-?");
            var language = cla.Argument("language", "The sample's programming language.");
            var validatorKey = cla.Argument("validatorKey", "The ValidatorAttribute.Key of the validator method.");
            var validatorAssemblyPath = cla.Argument("validatorAssemblyPath", "Absolute path to the assembly containing the validator.");
            var dependencies = cla.Option("-d|--dependencies", "The file paths of the code dependencies the sample has.", CommandOptionType.MultipleValue);
            cla.OnExecute(() =>
            {
                try
                {
                    Assembly validatorAssembly = LoadValidatorAssembly(validatorAssemblyPath.Value);
                    var invoker = new ValidatorInvoker(validatorAssembly, validatorKey.Value, language.Value, dependencies.Values);
                    Console.Out.WriteLine($"\"{typeof(ValidatorTimeout).AssemblyQualifiedName}\" 4000");
                    var errors = invoker.Invoke(ReadCode());
                    foreach (var error in errors)
                    {
                        Console.Out.WriteLine(error.ToString());
                    }
                    Console.ReadLine();
                    return 0;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(GetExceptionOutput(exception));
                    Console.ReadLine();
                    return 1;
                }
            });
        }

        private static string GetExceptionOutput(Exception exception)
        {
            return $"\"{exception.GetType().AssemblyQualifiedName}\" \"{exception.Message}\"";
        }

        private static Assembly LoadValidatorAssembly(string validatorAssemblyPath)
        {
            if (!string.IsNullOrEmpty(validatorAssemblyPath) && File.Exists(validatorAssemblyPath))
            {
                return Assembly.LoadFrom(validatorAssemblyPath);
            }
            else
            {
                throw new ArgumentException($"Assembly at '{validatorAssemblyPath}' could not be loaded.");
            }
        }

        private static string ReadCode()
        {
            return Console.In.ReadToEnd();
        }
    }
}