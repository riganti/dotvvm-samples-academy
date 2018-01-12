using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    public static class CSharpNameFormatterExtensions
    {
        public static string GetComplexInvokableName(this ICSharpNameFormatter formatter, string baseName, string memberName, IEnumerable<string> genericParameters, IEnumerable<string> parameterTypes)
        {
            var finalName = formatter.GetComplexName(baseName, memberName, genericParameters);
            return finalName = formatter.GetInvokableName(finalName, parameterTypes);
        }

        public static string GetComplexInvokableName(this ICSharpNameFormatter formatter, string baseName, string memberName, IEnumerable<CSharpGenericParameterDescriptor> genericParameters, IEnumerable<CSharpTypeDescriptor> parameters)
        {
            return formatter.GetComplexInvokableName(baseName, memberName, genericParameters?.Select(p => p.Name), parameters?.Select(p=>p.FullName));
        }

        public static string GetComplexName(this ICSharpNameFormatter formatter, string baseName, string memberName, IEnumerable<string> genericParameters)
        {
            var finalName = formatter.AppendMember(baseName, memberName);
            finalName = formatter.GetGenericName(finalName, genericParameters);
            return finalName;
        }

        public static string GetComplexName(this ICSharpNameFormatter formatter, string baseName, string memberName, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            return formatter.GetComplexName(baseName, memberName, genericParameters?.Select(p => p.Name));
        }

        public static string GetGenericName(this ICSharpNameFormatter formatter, string baseName, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            return formatter.GetGenericName(baseName, genericParameters?.Select(p => p.Name));
        }

        public static string GetInvokableName(this ICSharpNameFormatter formatter, string baseName, IEnumerable<CSharpTypeDescriptor> parameters)
        {
            return formatter.GetInvokableName(baseName, parameters?.Select(p => p.FullName));
        }
    }
}