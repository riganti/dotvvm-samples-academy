using System;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace DotvvmAcademy.Steps.Validation.Validators
{
    public static class ValidationExtensions
    {
        public static void ExecuteSafe(Action action)
        {
            try
            {
                action();
            }
            catch (RuntimeBinderException ex)
            {
                throw new CodeValidationException(ValidationErrorMessages.CommandMethodError, ex);
            }
            catch (CodeValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //todo UI exception
                throw new CodeValidationException("Bad UI exception",ex);
            }
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetTypeInfo().GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

    }
}