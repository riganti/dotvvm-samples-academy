using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public class ValidatorDelegateFactory
    {
        public const int ValidatorTimeOut = 1000;

        public ValidatorDelegateFactory()
        {
        }

        public ValidatorDelegate CreateCSharpValidator(MethodInfo validatorInfo)
        {
            return CreateValidator(validatorInfo, (code, dependencies) => new CSharpValidate(validatorInfo, code, dependencies));
        }

        public ValidatorDelegate CreateDothtmlValidator(MethodInfo validatorInfo)
        {
            return CreateValidator(validatorInfo, (code, dependencies) => new DothtmlValidate(validatorInfo, code, dependencies));
        }

        private ValidatorDelegate CreateValidator<TValidate>(MethodInfo method, Func<string, IEnumerable<string>, TValidate> getValidate)
            where TValidate : Validate
        {
            var parameters = method.GetParameters();
            CheckValidateParameter(method, typeof(TValidate), parameters);

            return (code, dependencies) =>
            {
                TValidate validate = getValidate(code, dependencies ?? Enumerable.Empty<string>());
                try
                {
                    method.Invoke(null, new[] { validate });
                    return validate.ValidationErrors;
                }
                catch (Exception e)
                {
                    throw new ValidatorDesignException($"An exception caused by faulty design of the validator occured during " +
                        $"the execution of the '{method.Name}' validator method.", method, e);
                }
            };
        }

        private void CheckValidateParameter(MethodInfo method, Type expectedValidateType, ParameterInfo[] parameters)
        {
            var validateParameters = parameters.Where(p => expectedValidateType.IsAssignableFrom(p.ParameterType)).ToList();
            if (validateParameters.Count > 1)
            {
                throw new ValidatorDesignException($"Cannot create validator '{method.Name}' " +
                    $"as it has multiple '{expectedValidateType.Name}' parameters.", method);
            }

            if (validateParameters.Count == 0)
            {
                throw new ValidatorDesignException($"Cannot create validator '{method.Name}' " +
                    $"as it has no '{expectedValidateType.Name}' parameters. Are you sure this is the " +
                    $"right validator for the sample's programming language?", method);
            }
        }
    }
}