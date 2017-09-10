using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Cli.Host
{
    public class ValidatorCliArguments
    {
        public ValidatorCliArguments(string codeLanguage, string validatorKey, string validatorAssemblyPath)
        {
            CodeLanguage = codeLanguage;
            ValidatorKey = validatorKey;
            ValidatorAssemblyPath = validatorAssemblyPath;
        }

        public string CodeLanguage { get; }

        public List<string> Dependencies { get; set; }

        public string ValidatorAssemblyPath { get; }

        public string ValidatorKey { get; }

        internal string ValidatorCliPath { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(ValidatorCliPath);
            sb.Append(' ');
            sb.Append(CodeLanguage);
            sb.Append(' ');
            sb.Append(ValidatorKey);
            sb.Append(' ');
            sb.Append($"\"{ValidatorAssemblyPath}\"");
            if (Dependencies?.Count > 0)
            {
                sb.Append(' ');
                sb.Append("-d");
                for (int i = 0; i < Dependencies.Count; i++)
                {
                    sb.Append(' ');
                    sb.Append($"\"{Dependencies[i]}\"");
                }
            }
            return sb.ToString();
        }
    }
}