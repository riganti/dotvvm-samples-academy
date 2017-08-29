using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml
{
    public static class DothtmlPropertyExtensions
    {
        public static void CommandBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.CommandBinding((IEnumerable<string>)expectedValues);

        public static void ControlCommandBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.ControlCommandBinding((IEnumerable<string>)expectedValues);

        public static void ControlPropertyBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.ControlPropertyBinding((IEnumerable<string>)expectedValues);

        public static void HardcodedValue(this DothtmlProperty p, params string[] expectedValues)
            => p.HardcodedValue((IEnumerable<string>)expectedValues);

        public static void ResourceBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.ResourceBinding((IEnumerable<string>)expectedValues);

        public static void StaticCommandBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.StaticCommandBinding((IEnumerable<string>)expectedValues);

        public static void ValueBinding(this DothtmlProperty p, params string[] expectedValues)
            => p.ValueBinding((IEnumerable<string>)expectedValues);
    }
}