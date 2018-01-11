using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Validators;
using DotvvmAcademy.DAL.Providers;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class ValidatorFacade : IFacade
    {
        private readonly SampleProvider sampleProvider;
        private IDictionary<string, MethodInfo> csharpUnitValidators;
        private Assembly validatorsAssembly = typeof(BasicMvvmValidators).Assembly;

        public ValidatorFacade(SampleProvider sampleProvider)
        {
            this.sampleProvider = sampleProvider;
            csharpUnitValidators = ValidationUtilities.GetValidationMethods<ICSharpDocument>(typeof(BasicMvvmValidators).Assembly);
        }

        public async Task<IEnumerable<ValidationErrorDto>> Validate(ExerciseStepPartDto exerciseDto, string code)
        {
            var codeLanguage = exerciseDto.CodeLanguage.ToString().ToLower();
            if (exerciseDto is CSharpExerciseStepPartDto csharpExercise)
            {
                var sources = new string[csharpExercise.DependencyPaths.Length + 1];
                sources[0] = code;
                for (int i = 0; i < csharpExercise.DependencyPaths.Length; i++)
                {
                    var path = csharpExercise.DependencyPaths[i];
                    sources[i + 1] = (await sampleProvider.Get(path)).Source;
                }
                var response = await ValidateCSharp(csharpExercise.ValidatorId, sources, new[] { csharpExercise.DisplayName });
                return response.Diagnostics.Select(e => Mapper.Map<ValidationDiagnostic, ValidationErrorDto>(e));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private async Task<CSharpValidationResponse> ValidateCSharp(string validationMethodName, string[] sources, string[] sourceNames)
        {
            var method = csharpUnitValidators[validationMethodName];
            var request = CSharpValidationUtilities.CreateRequest(sources, sourceNames);
            var validator = CSharpValidationUtilities.CreateValidator();
            var runner = CSharpValidationUtilities.CreateRunner();
            (request.StaticAnalysis, request.DynamicAnalysis) = runner.Run(method);
            return await validator.Validate(request);
        }
    }
}