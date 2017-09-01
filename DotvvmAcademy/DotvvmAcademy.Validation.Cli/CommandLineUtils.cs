using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Cli
{
    public class CommandLineUtils
    {
        public static string[] ParseArguments(string argsString)
        {
            var argsChars = argsString.ToCharArray();
            var args = new List<string>();
            bool inQuote = false;
            bool isEscaped = false;
            int lastIndex = 0;
            for (int i = 0; i < argsChars.Length; i++)
            {
                var character = argsChars[i];
                isEscaped = character == '\\' && inQuote;

                if (character == '\"')
                {
                    if (isEscaped) continue;
                    inQuote = !inQuote;
                    continue;
                }

                if ((!inQuote && character == ' ') || i == argsChars.Length - 1)
                {
                    if (i == argsChars.Length - 1) i++;
                    var length = i - lastIndex;
                    args.Add(argsString.Substring(lastIndex, length));
                    lastIndex = i + 1;
                }
            }
            return args.Select(a => a.TrimStart('\"').TrimEnd('\"')).ToArray();
        }
    }
}