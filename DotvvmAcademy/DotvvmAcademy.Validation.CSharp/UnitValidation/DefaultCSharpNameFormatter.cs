using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpNameFormatter : ICSharpNameFormatter
    {
        public string AppendMember(string baseName, string memberName)
        {
            return $"{baseName}.{memberName}";
        }

        public string GetGenericName(string baseName, IEnumerable<string> genericParameters)
        {
            if (genericParameters == null || genericParameters.Count() == 0)
            {
                return baseName;
            }

            return GetStringList(baseName, "<", ">", ", ", genericParameters);
        }

        public string GetInvokableName(string baseName, IEnumerable<string> parameterTypes)
        {
            return GetStringList(baseName, "(", ")", ", ", parameterTypes);
        }

        public string GetMemberName(string fullName)
        {
            var startIndex = fullName.LastIndexOf('.') + 1;
            fullName = fullName.Substring(startIndex, fullName.Length - startIndex);
            var invokableIndex = fullName.LastIndexOf("<");
            var genericIndex = fullName.LastIndexOf("(");
            int endIndex = genericIndex == -1 ? invokableIndex : genericIndex;
            if (endIndex != -1)
            {
                fullName = fullName.Substring(0, endIndex + 1);
            }
            return fullName;
        }

        private string GetStringList(string baseName, string start, string end, string separator, IEnumerable<string> strings)
        {
            var stringList = strings?.ToList();
            var sb = new StringBuilder();
            sb.Append(baseName);
            sb.Append(start);
            for (int i = 0; i < stringList?.Count; i++)
            {
                var parameter = stringList[i];
                sb.Append(parameter);
                if (i < stringList.Count - 1)
                {
                    sb.Append(separator);
                }
            }
            sb.Append(end);
            return sb.ToString();
        }
    }
}