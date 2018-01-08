using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public interface IValidationMethodLocator
    {
        IEnumerable<MethodInfo> GetMethods(Assembly assembly);

        IEnumerable<MethodInfo> GetMethods<TDocument>(Assembly assembly)
            where TDocument : IDocument;
    }
}