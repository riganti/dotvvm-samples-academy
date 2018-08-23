using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    // TODO: Move to Meta once Memory becomes available on netstandard
    public class PositionConverter : IPositionConverter
    {
        private readonly ImmutableArray<ReadOnlyMemory<char>> lines;

        public PositionConverter(string source)
        {
            lines = GetLines(source.AsMemory());
        }

        public (int lineNumber, int column) ToCoords(int index)
        {
            if (index == -1)
            {
                return (-1, -1);
            }

            var lastSum = 0;
            var sum = 0;
            var lineIndex = -1;
            ReadOnlyMemory<char> line;
            do
            {
                lineIndex++;
                line = lines[lineIndex];
                lastSum = sum;
                sum += line.Length;
            }
            while (sum <= index);

            return (lineIndex + 1, index - lastSum + 1);
        }

        public int ToIndex(int lineNumber, int column)
        {
            if ((lineNumber, column) == (-1, -1))
            {
                return -1;
            }

            for (int i = 0; i < lineNumber; i++)
            {
                column += lines[i].Length;
            }

            return column;
        }

        private ImmutableArray<ReadOnlyMemory<char>> GetLines(ReadOnlyMemory<char> source)
        {
            var builder = ImmutableArray.CreateBuilder<ReadOnlyMemory<char>>();
            var start = 0;
            int end;
            for (end = 0; end < source.Length; end++)
            {
                if (source.Span[end] == '\n')
                {
                    builder.Add(source.Slice(start, end - start + 1));
                    start = end + 1;
                }
            }
            builder.Add(source.Slice(start, end - start));
            return builder.ToImmutable();
        }
    }
}