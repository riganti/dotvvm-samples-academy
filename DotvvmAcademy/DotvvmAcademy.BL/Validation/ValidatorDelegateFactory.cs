using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Validation.CSharp;
using DotvvmAcademy.BL.Validation.Dothtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.BL.Validation
{
    public class ValidatorDelegateFactory
    {
        private IServiceProvider serviceProvider;

        public ValidatorDelegateFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ValidatorDelegate CreateValidator(MethodInfo validatorInfo, SampleCodeLanguage sampleLanguage)
        {
            switch (sampleLanguage)
            {
                case SampleCodeLanguage.CSharp:
                    return CreateValidator<CSharpValidate>(validatorInfo);

                case SampleCodeLanguage.Dothtml:
                    return CreateValidator<DothtmlValidate>(validatorInfo);

                default:
                    throw new NotSupportedException($"Validator for {nameof(SampleCodeLanguage)}.{sampleLanguage.ToString()} cannot be created.");
            }
        }

        public ValidatorDelegate CreateValidator<TValidate>(MethodInfo validatorInfo)
            where TValidate : Validate
        {
            var parameters = validatorInfo.GetParameters();
            ValidateParameters<TValidate>(validatorInfo, parameters);
            return (code, dependencies) =>
            {
                try
                {
                    Validate validate = (Validate)Activator.CreateInstance(typeof(TValidate), code, dependencies ?? Enumerable.Empty<string>());
                    List<object> arguments = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        if (parameter.ParameterType == typeof(TValidate))
                        {
                            arguments.Add(validate);
                        }
                        else
                        {
                            arguments.Add(serviceProvider.GetService(parameter.ParameterType));
                        }
                    }
                    validatorInfo.Invoke(null, arguments.ToArray());
                    return validate.ValidationErrors;
                }
                catch (Exception e)
                {
                    throw new ValidatorException($"An exception occured during the execution of the '{validatorInfo.Name}' validator method.",
                        validatorInfo, e);
                }
            };
        }

        private void ValidateParameters<TValidate>(MethodInfo validatorInfo, ParameterInfo[] parameters)
        {
            var validates = parameters.Where(p => typeof(Validate).IsAssignableFrom(p.ParameterType)).ToList();
            if (validates.Count > 1)
            {
                throw new NotSupportedException($"Cannot create validator '{validatorInfo.Name}' as it has multiple parameters inheriting from {nameof(Validate)}.");
            }

            if (validates.Count == 0)
            {
                throw new NotSupportedException($"Cannot create validator '{validatorInfo.Name}' as it has no parameters inherting from {nameof(Validate)}.");
            }

            var validateType = validates.Single().ParameterType;
            if (validateType != typeof(TValidate))
            {
                throw new NotSupportedException($"Cannot create validator '{validatorInfo.Name}' as its parameter " +
                    $"of type {validateType.Name} is not supported for the programming language of the validated sample.");
            }
        }
    }
}