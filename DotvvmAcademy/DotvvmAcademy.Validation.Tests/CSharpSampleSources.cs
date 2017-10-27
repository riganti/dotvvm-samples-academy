namespace DotvvmAcademy.Validation.Tests
{
    public static class CSharpSampleSources
    {
        public const string Sample = @"
        public class Test
        {
            public int TestProperty { get; set; }

            public string TestMethod()
            {
                TestProperty = 42;
                return ""Sample output"";
            }
        }";
    }
}