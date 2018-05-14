using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlIdentifierParser
    {
        private readonly char[] digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly char[] specialChars = new[] { '/', '[', ']' };
        private DothtmlIdentifier current;
        private int? index;
        private string input;
        private int position;

        public DothtmlIdentifier Parse(string path)
        {
            position = 0;
            input = path;
            index = null;
            current = null;
            string segment = null;
            while (position < input.Length)
            {
                switch (path[position])
                {
                    case '/':
                        current = new DothtmlIdentifier(segment, index, current);
                        break;

                    case '[':
                        index = ReadIndex();
                        break;

                    case ']':
                        break;

                    default:
                        segment = ReadSegment();
                        continue;
                }
                position++;
            }
            return new DothtmlIdentifier(segment, index, current);
        }

        private int ReadIndex()
        {
            int start = position;
            while (digits.Contains(input[position]))
            {
                position++;
            }
            return int.Parse(input.Substring(start, position - start));
        }

        private string ReadSegment()
        {
            int start = position;
            while (!specialChars.Contains(input[position]))
            {
                position++;
            }
            return input.Substring(start, position - start);
        }
    }
}