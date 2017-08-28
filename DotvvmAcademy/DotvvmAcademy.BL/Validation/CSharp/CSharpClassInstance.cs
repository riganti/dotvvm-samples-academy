using System;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClassInstance : ValidationObject<CSharpValidate>
    {
        internal CSharpClassInstance(CSharpValidate validate, object rawInstance, bool isActive = true) : base(validate, isActive)
        {
            if (!isActive) return;
            RawInstance = rawInstance;
        }

        public static CSharpClassInstance Inactive => new CSharpClassInstance(null, null, false);

        public object RawInstance { get; }

        public override void AddError(string message) => AddGlobalError(message);

        public void MethodCall(CSharpMethod method, object expectedResult = null, params object[] arguments)
        {
            if (!IsActive || !method.IsActive) return;

            var methodInfo = RawInstance.GetType().GetMethod(method.Symbol.Name);
            var result = methodInfo.Invoke(RawInstance, arguments);
            if (result != expectedResult)
            {
                method.AddError($"The '{method.Symbol.Name}' method produced an unexpected result: '{result}'. Expected: '{expectedResult}'.");
            }
        }

        public void PropertyGetter(CSharpProperty property, object expectedValue, string customErrorMessage = null)
        {
            Func<object, string> geErrorMessage = (value) =>
            {
                return customErrorMessage ?? $"The '{property.Symbol.Name}' property " +
                $"contains an unexpected value: '{value}'. Expected: '{expectedValue}'.";
            };
            PropertyGetter(property, o => o?.Equals(expectedValue) == true, geErrorMessage);
        }

        public void PropertyGetter(CSharpProperty property, Predicate<object> isValid, Func<object, string> getErrorMessage)
        {
            if (!IsActive || !property.IsActive) return;

            var propertyInfo = RawInstance.GetType().GetProperty(property.Symbol.Name);
            var value = propertyInfo.GetValue(RawInstance);
            if (!isValid(value))
            {
                property.AddError(getErrorMessage(value));
            }
        }

        public void PropertySetter(CSharpProperty property, object value)
        {
            if (!IsActive || !property.IsActive) return;

            var propertyInfo = RawInstance.GetType().GetProperty(property.Symbol.Name);
            propertyInfo.SetValue(RawInstance, value);
        }
    }
}