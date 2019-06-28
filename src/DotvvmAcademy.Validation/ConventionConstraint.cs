using System;
using System.Linq.Expressions;

namespace DotvvmAcademy.Validation
{
    public class ConventionConstraint<TConstraint> : IConstraint
    {
        private const string ValidateMethodName = "Validate";

        private static readonly Action<IServiceProvider, TConstraint> ValidateAction = GetValidateAction();
        private readonly TConstraint instance;

        public ConventionConstraint(TConstraint instance)
        {
            this.instance = instance;
        }

        public void Validate(IServiceProvider services)
        {
            ValidateAction(services, instance);
        }

        private static Action<IServiceProvider, TConstraint> GetValidateAction()
        {
            var methodInfo = typeof(TConstraint).GetMethod(ValidateMethodName);
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Type \"{typeof(TConstraint)}\" doesn't contain a public Validate method.");
            }
            var parameters = methodInfo.GetParameters();

            var servicesArg = Expression.Parameter(typeof(IServiceProvider), "services");
            var instanceArg = Expression.Parameter(typeof(TConstraint), "instance");
            var callArgs = new Expression[parameters.Length];
            var getService = typeof(IServiceProvider).GetMethod(nameof(IServiceProvider.GetService));
            for (int i = 0; i < callArgs.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef)
                {
                    throw new NotSupportedException("Invoke doesn't support ref or out parameters.");
                }
                var getServiceCall = Expression.Call(servicesArg, getService, Expression.Constant(parameterType, typeof(Type)));
                callArgs[i] = Expression.Convert(getServiceCall, parameterType);
            }
            var body = Expression.Call(instanceArg, methodInfo, callArgs);
            var lambda = Expression.Lambda<Action<IServiceProvider, TConstraint>>(body, servicesArg, instanceArg);
            return lambda.Compile();
        }
    }
}