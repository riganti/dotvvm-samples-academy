using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.Cli
{
    public class ValidatorInvoker
    {
        private ValidatorDelegateFactory factory = new ValidatorDelegateFactory();
        private MethodInfo method;
        private ValidatorInvocation invocation;
        private IEnumerable<string> dependencyPaths;

        private delegate IEnumerable<ValidationError> ValidatorInvocation(MethodInfo method, string code, IEnumerable<string> dependencyPaths);

        public ValidatorInvoker(Assembly validatorAssembly, string validatorKey, string codeLanguage, IEnumerable<string> dependencyPaths)
        {
            invocation = GetValidatorInvocation(codeLanguage);
            method = GetValidatorMethod(validatorAssembly, validatorKey);
            this.dependencyPaths = dependencyPaths;
        }

        public IEnumerable<ValidationError> Invoke(string code)
        {
            return invocation(method, code, dependencyPaths);
        }

        private IEnumerable<ValidationError> InvokeCSharpValidator(MethodInfo method, string code, IEnumerable<string> dependencyPaths)
        {
            var dependencies = dependencyPaths.Select(path => File.ReadAllText(path));
            return factory.CreateCSharpValidator(method)(code, dependencies);
        }

        private IEnumerable<ValidationError> InvokeDothtmlValidator(MethodInfo method, string code, IEnumerable<string> dependencyPaths)
        {
            var dependencies = dependencyPaths.Select(path => File.ReadAllText(path));
            return factory.CreateDothtmlValidator(method)(code, dependencies);
        }

        private ValidatorInvocation GetValidatorInvocation(string codeLanguage)
        {
            switch (codeLanguage)
            {
                case "csharp":
                    return InvokeCSharpValidator;

                case "dothtml":
                    return InvokeDothtmlValidator;

                default:
                    throw new NotSupportedException($"The Validator CLI doesn't support the '{codeLanguage}' programming language.");
            }
        }

        private MethodInfo GetValidatorMethod(Assembly assembly, string validatorKey)
        {
            var methods = assembly.GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Select(m => (MethodInfo: m, Attribute: m.GetCustomAttribute<ValidatorAttribute>()))
                .Where(o => o.Attribute != null && o.Attribute.Key == validatorKey)
                .Select(o => o.MethodInfo)
                .ToList();
            if (methods.Count == 0)
            {
                throw new ValidatorDesignException($"A Validator with key '{validatorKey}' couldn't be found.");
            }

            if (methods.Count > 1)
            {
                throw new ValidatorDesignException($"Multiple Validators with id '{validatorKey}' found.");
            }

            return methods.Single();
        }
    }
}