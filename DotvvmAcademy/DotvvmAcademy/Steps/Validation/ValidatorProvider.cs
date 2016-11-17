using System;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators;

namespace DotvvmAcademy.Steps.Validation
{
    public class ValidatorProvider<T> where T : ILessonValidationObject
    {
        public T CreateValidator(string validatorKey)
        {
            var validatorClass =
                typeof(LessonBase).GetTypeInfo()
                    .Assembly.GetTypes()
                    .Where(
                        a =>
                            (a.Namespace != null) &&
                            a.Namespace.Contains(@"DotvvmAcademy.Steps.Validation.Validators.Lesson1"))
                    .First(
                        c => c.GetAttributeValue((StepValidationAttribute test) => test.ValidationKey == validatorKey));


            return (T) Activator.CreateInstance(validatorClass);
        }
    }
}