using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Cli.Host
{
    internal class ValidatorCliLineProcessor
    {
        public TObject ProcessLine<TObject>(string line)
        {
            var lineObject = ProcessLine(line);
            if (lineObject is TObject castObject)
            {
                return castObject;
            }
            else
            {
                throw new ValidatorCliException($"An unexpected object of type '{lineObject.GetType().Name}' " +
                    $"was processed. Expected type was '{typeof(TObject).Name}'.");
            }
        }

        public object ProcessLine(string line)
        {
            var args = CommandLineUtils.ParseArguments(line);
            var type = Type.GetType(args[0]);
            if(type == null)
            {
                throw new ValidatorCliException($"The type '{args[0]}' couldn't be resolved.");
            }

            return GetObject(type, args.Skip(1).ToList());
        }

        private object GetObject(Type type, List<string> arguments)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length < arguments.Count)
                {
                    continue;
                }
                var requiredParameterCount = parameters.Where(p => !p.IsOptional).Count();
                if (requiredParameterCount > arguments.Count)
                {
                    continue;
                }

                var convertedArguments = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    if(i >= arguments.Count)
                    {
                        convertedArguments[i] = null;
                        continue;
                    }

                    var argument = arguments[i];
                    var parameter = parameters[i];
                    try
                    {
                        convertedArguments[i] = Convert.ChangeType(argument, parameter.ParameterType);
                    }
                    catch
                    {
                        break;
                    }
                }
                if (convertedArguments.Length == parameters.Length)
                {
                    return constructor.Invoke(convertedArguments);
                }
                else
                {
                    continue;
                }
            }

            var argumentString = string.Join(", ", arguments);
            throw new ValidatorCliException($"An object of type '{type.Name}' cannot be constructed with these arguments: '{argumentString}'.");
        }
    }
}