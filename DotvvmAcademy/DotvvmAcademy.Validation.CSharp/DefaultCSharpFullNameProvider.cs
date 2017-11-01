using DotvvmAcademy.Validation.CSharp.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpFullNameProvider : ICSharpFullNameProvider
    {
        public string GetGenericName(string baseName, IEnumerable<string> genericParameters)
        {
            return GetStringList(baseName, "<", ">", ", ", genericParameters);
        }

        public string GetInvokableName(string baseName, IEnumerable<string> parameterTypes)
        {
            return GetStringList(baseName, "(", ")", ", ", parameterTypes);
        }

        public string GetMemberName(string baseName, string memberName)
        {
            return $"{baseName}.{memberName}";
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