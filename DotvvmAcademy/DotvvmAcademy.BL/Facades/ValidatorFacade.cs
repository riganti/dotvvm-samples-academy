using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.BL.Facades
{
    public sealed class ValidatorFacade
    {
        private ValidatorDelegateFactory factory;

        public ValidatorFacade(ValidatorDelegateFactory validatorDelegateFactory)
        {
            factory = validatorDelegateFactory;
        }

        public ValidatorDelegate GetValidator(string id, SampleCodeLanguage language)
        {
            var validators = GetTypesFromValidatorNamespace()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Select(m => (MethodInfo: m, Attribute: m.GetCustomAttribute<ValidatorAttribute>()))
                .Where(o => o.Attribute != null && o.Attribute.Id == id)
                .Select(o => o.MethodInfo)
                .ToList();

            if (validators.Count == 0)
            {
                throw new ArgumentException($"A Validator with id '{id}' could not be found.", nameof(id));
            }

            if (validators.Count > 1)
            {
                throw new ArgumentException($"Multiple Validators with id '{id}' were found.", nameof(id));
            }

            return factory.CreateValidator(validators.Single(), language);
        }

        private IEnumerable<Type> GetTypesFromValidatorNamespace()
        {
            return typeof(ValidatorFacade).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.Namespace == $"{nameof(DotvvmAcademy)}.{nameof(BL)}.{nameof(Validators)}");
        }
    }
}