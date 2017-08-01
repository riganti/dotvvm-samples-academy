using System;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.ValidatorProvision
{
    /// <summary>
    ///     Provider for creating instance of validatorClass.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatorProvider<T> where T : ILessonValidationObject
    {
        /// <summary>
        /// Create instance of validator class by validatorKey and validatorNamespace.
        /// </summary>
        /// <param name="validatorKey"></param>
        /// <param name="validatorNamespace"></param>
        public T CreateValidator(string validatorKey, string validatorNamespace)
        {
            var validatorClass = FindValidatorByKey(validatorKey, validatorNamespace);

            return (T)Activator.CreateInstance(validatorClass);
        }

        /// <summary>
        /// Find validator class which have StepValidationAttribute.ValidatorKey == validatorKey in right validatorNamespace.
        /// </summary>
        /// <param name="validatorKey"></param>
        /// <param name="validatorNamespace"></param>
        private static Type FindValidatorByKey(string validatorKey, string validatorNamespace)
        {
            return typeof(LessonBase)
                .GetTypeInfo()
                .Assembly.GetTypes()
                .Where(a =>
                    a.Namespace != null &&
                    a.Namespace.Contains(validatorNamespace))
                .First(c => c.GetAttributeValue((StepValidationAttribute sva) => sva.ValidatorKey == validatorKey));
        }
    }
}