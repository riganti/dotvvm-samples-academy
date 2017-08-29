using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.Lessons.Validators;
using DotvvmAcademy.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public sealed class ValidatorFacade
    {
        private readonly ValidatorDelegateFactory factory;
        private readonly SampleFacade sampleFacade;

        public ValidatorFacade(ValidatorDelegateFactory factory, SampleFacade sampleFacade)
        {
            this.factory = factory;
            this.sampleFacade = sampleFacade;
        }

        public async Task<IEnumerable<ValidationErrorDTO>> Validate(SampleDTO dto, string code)
        {
            var validators = GetTypesFromValidatorNamespace()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Select(m => (MethodInfo: m, Attribute: m.GetCustomAttribute<ValidatorAttribute>()))
                .Where(o => o.Attribute != null && o.Attribute.Key == dto.ValidatorKey)
                .Select(o => o.MethodInfo)
                .ToList();

            if (validators.Count == 0)
            {
                throw new ArgumentException($"A Validator with key '{dto.ValidatorKey}' could not be found.", nameof(dto));
            }

            if (validators.Count > 1)
            {
                throw new ArgumentException($"Multiple Validators with id '{dto.ValidatorKey}' were found.", nameof(dto));
            }

            switch (dto.CodeLanguage)
            {
                case SampleCodeLanguage.CSharp:
                    return (await CallCSharpValidator(validators.Single(), dto, code)).Select(e => ValidationErrorDTO.Create(e));

                case SampleCodeLanguage.Dothtml:
                    return (await CallDothtmlValidator(validators.Single(), dto, code)).Select(e => ValidationErrorDTO.Create(e));

                default:
                    throw new NotSupportedException($"The SampleCodeLanguage '{dto.CodeLanguage.ToString()}' " +
                        $"is not supported by the {nameof(ValidatorFacade)}");
            }
        }

        private async Task<IEnumerable<ValidationError>> CallCSharpValidator(MethodInfo method, SampleDTO dto, string code)
        {
            var dependencies = dto.Dependencies.Select(path => sampleFacade.GetRawSample(dto.LessonIndex, dto.Language, dto.StepIndex, path));
            return await factory.CreateCSharpValidator(method)(code, dependencies);
        }

        private async Task<IEnumerable<ValidationError>> CallDothtmlValidator(MethodInfo method, SampleDTO dto, string code)
        {
            var dependencies = dto.Dependencies.Select(path => sampleFacade.GetRawSample(dto.LessonIndex, dto.Language, dto.StepIndex, path));
            return await factory.CreateDothtmlValidator(method)(code, dependencies);
        }

        private IEnumerable<Type> GetTypesFromValidatorNamespace()
        {
            return typeof(BasicMvvmValidators).GetTypeInfo().Assembly.GetTypes();
        }
    }
}