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

        public const string NeverEndingSample = @"
        public class Test
        {
            public void TestMethod()
            {
                var s = 0;
                while(true)
                {
                    s++;
                }
            }
        }";

        public const string NeverEndingSample2 = @"
        public class Test
        {
            public void TestMethod()
            {
                while(true)
                {
                }
            }
        }";
    }
}