using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Validation;
using System;
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
            var validators = typeof(ValidatorFacade).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.Namespace == $"{nameof(DotvvmAcademy)}.{nameof(BL)}.{nameof(Validators)}")
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public));
            foreach (var validator in validators)
            {
                var attribute = validator.GetCustomAttribute<ValidatorAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                if (attribute.Id == id)
                {
                    return factory.CreateValidator(validator, language);
                }
            }

            throw new ArgumentException($"A Validator with id '{id}' could not be found.", nameof(id));
        }
    }
}