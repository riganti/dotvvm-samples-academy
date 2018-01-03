using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Providers;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.Abstractions;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class ValidatorFacade : IFacade
    {
        private readonly ValidatorAssemblyProvider validatorAssemblyProvider;
        private readonly SampleProvider sampleProvider;
        private IDictionary<string, MethodInfo> csharpValidationMethods;

        public ValidatorFacade(ValidatorAssemblyProvider validatorAssemblyProvider, SampleProvider sampleProvider)
        {
            this.validatorAssemblyProvider = validatorAssemblyProvider;
            this.sampleProvider = sampleProvider;
        }

        public async Task<IEnumerable<ValidationErrorDto>> Validate(ExerciseStepPartDto exerciseDto, string code)
        {
            var codeLanguage = exerciseDto.CodeLanguage.ToString().ToLower();
            if(exerciseDto is CSharpExerciseStepPartDto csharpExercise)
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
            csharpValidationMethods = csharpValidationMethods ?? await GetValidationMethods<ICSharpDocument>();
            var method = csharpValidationMethods[validationMethodName];
            var request = CSharpValidationUtilities.CreateRequest(sources, sourceNames);
            var validator = CSharpValidationUtilities.CreateValidator();
            var runner = CSharpValidationUtilities.CreateRunner();
            (request.StaticAnalysis, request.DynamicAnalysis) = runner.Run(method);
            return await validator.Validate(request);
        }

        private async Task<IDictionary<string, MethodInfo>> GetValidationMethods<TDocument>()
            where TDocument : IDocument
        {
            var validatorAssembly = await validatorAssemblyProvider.Get();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(validatorAssembly.AbsolutePath);
            return ValidationUtilities.GetValidationMethods<TDocument>(assembly);
        }
    }
}