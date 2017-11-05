using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpFullNameProvider
    {
        string GetMemberName(string baseName, string memberName);

        string GetInvokableName(string baseName, IEnumerable<string> parameterTypes);

        string GetGenericName(string baseName, IEnumerable<string> genericParameters);
    }
}