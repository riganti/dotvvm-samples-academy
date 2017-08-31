using DotvvmAcademy.Lessons.Validators;
using Microsoft.Extensions.CommandLineUtils;
using System;

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
            var dependencies = cla.Option("-d|--dependencies", "The file paths of the code dependencies the sample has.", CommandOptionType.MultipleValue);
            cla.OnExecute(() =>
            {
                try
                {
                    var validatorAssembly = typeof(BasicMvvmValidators).Assembly;
                    var invoker = new ValidatorInvoker(validatorAssembly, validatorKey.Value, language.Value, dependencies.Values);
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
            return $"{exception.GetType().Name} \"{exception.Message}\"";
        }

        private static string ReadCode()
        {
            return Console.In.ReadToEnd();
        }
    }
}