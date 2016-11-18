using System;
using System.IO;
using System.Xml.Linq;
using DotvvmAcademy.Steps;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Interfaces;

namespace DotvvmAcademy.Helpers
{
    public class StepBuilder
    {
        public StepBase CreateStep(XElement stepElement, int index)
        {
            //todo to resources
            var csharpcode = "CsharpCode";
            var dothtmlcode = "DothtmlCode";
            var info = "Info";
            var type = "Type";


            var stepType = stepElement.GetAttribute(type).Value;

            if (stepType == csharpcode)
            {
                return CreateCodeCsharpStep(stepElement, index);
            }
            if (stepType == dothtmlcode)
            {
                return CreateCodeDothtmlStep(stepElement, index);
            }

            if (stepType == info)
            {
                return CreateInfoStep(stepElement, index);
            }
            throw new InvalidDataException($"Step type {stepElement.Name} ins`t supported");
        }


        private CodeStepCsharp CreateCodeCsharpStep(XElement stepElement, int index)
        {
            var csharpStep = new CodeStepCsharp();
            FillStepCodeData(csharpStep, stepElement, index);
            CreateValidator(csharpStep, stepElement);
            FillAdditionalCsharpCodeData(csharpStep, stepElement);
            return csharpStep;
        }


        private CodeStepDotHtml CreateCodeDothtmlStep(XElement stepElement, int index)
        {
            var dothtmlStep = new CodeStepDotHtml();
            FillStepCodeData(dothtmlStep, stepElement, index);
            CreateValidator(dothtmlStep, stepElement);
            return dothtmlStep;
        }

        private InfoStep CreateInfoStep(XElement step, int index)
        {
            var infoStep = new InfoStep();
            FillStepBasicData(infoStep, step, index);
            return infoStep;
        }


        private void FillStepBasicData(IStep stepBase, XElement stepElement, int index)
        {
            //todo resources
            var childElementName = "Description";
            var elementName = "Title";

            stepBase.StepIndex = index;
            stepBase.Description = stepElement.GetChildElementStringValue(childElementName);
            stepBase.Title = stepElement.GetChildElementStringValue(elementName);
        }

        private void FillStepCodeData(ICodeStepData stepCodeBase, XElement stepElement, int index)
        {
            //todo resources
            var finalcode = "FinalCode";
            var startupcode = "StartupCode";
            var shadowboxdescription = "ShadowBoxDescription";

            FillStepBasicData(stepCodeBase, stepElement, index);
            stepCodeBase.StartupCode = stepElement.GetChildElementStringValue(startupcode);
            stepCodeBase.FinalCode = stepElement.GetChildElementStringValue(finalcode);
            if (stepElement.HaveChildElement(shadowboxdescription))
            {
                stepCodeBase.ShadowBoxDescription = stepElement.GetChildElementStringValue(shadowboxdescription);
            }
        }

        private void FillAdditionalCsharpCodeData(CodeStepCsharp csharpCode, XElement stepElement)
        {
            //todo resources
            var codeDependencies = "CodeDependencies";
            var codeDependency = "CodeDependency";

            var allowedTypes = "AllowedTypesConstructed";
            var allowedType = "AllowedType";

            var allowedMethods = "AllowedMethodsCalled";
            var allowedMethod = "AllowedMethod";


            Action<CodeStepCsharp, string> addAction;

            if (stepElement.HaveChildElement(codeDependencies))
            {
                addAction = (code, at) => code.OtherCodeDependencies.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, codeDependencies, codeDependency, addAction);
            }

            if (stepElement.HaveChildElement(allowedTypes))
            {
                addAction = (code, at) => code.AllowedTypesConstructed.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, allowedTypes, allowedType, addAction);
            }

            if (stepElement.HaveChildElement(allowedMethods))
            {
                addAction = (code, at) => code.AllowedMethodsCalled.Add(at);
                FillElementChildrenCollection(csharpCode, stepElement, allowedMethods, allowedMethod, addAction);
            }
        }


        private static void FillElementChildrenCollection(CodeStepCsharp csharpCode, XElement parentStepElement,
            string elementWithCollectionName, string itemElementName, Action<CodeStepCsharp, string> addAction)
        {
            var otherCodeDependencies =
                parentStepElement.Element(elementWithCollectionName).GetChildCollection(itemElementName);
            foreach (var dependency in otherCodeDependencies)
            {
                addAction.Invoke(csharpCode, dependency.GetElementStringValue());
            }
        }


        private void CreateValidator<T>(ICodeStep<T> result, XElement stepElement)
            where T : ILessonValidationObject
        {
            //todo resources
            var validationkey = "ValidationKey";
            var validatorfolder = "ValidatorFolder";

            var provider = new ValidatorProvider<T>();
            var validationKey = stepElement.GetChildElementStringValue(validationkey);
            var validationFolder = stepElement.Parent.Parent.GetChildElementStringValue(validatorfolder);
            result.Validator = provider.CreateValidator(validationKey, validationFolder);
        }
    }
}