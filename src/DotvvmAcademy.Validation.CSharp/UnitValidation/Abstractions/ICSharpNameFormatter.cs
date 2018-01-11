using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpNameFormatter
    {
        string AppendMember(string baseName, string memberName);

        string GetMemberName(string fullName);

        string GetInvokableName(string baseName, IEnumerable<string> parameterTypes);

        string GetGenericName(string baseName, IEnumerable<string> genericParameters);
    }
}