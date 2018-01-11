namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public static class CSharpStaticAnalysisContextExtensions
    {
        public static void AddMetadata<TAnalyzer>(this CSharpStaticAnalysisContext context, StaticAnalysisMetadataCollection metadata)
            where TAnalyzer : ValidationAnalyzer
        {
            context.AddMetadata(typeof(TAnalyzer), metadata);
        }

        public static StaticAnalysisMetadataCollection GetMetadata<TAnalyzer>(this CSharpStaticAnalysisContext context)
                    where TAnalyzer : ValidationAnalyzer
        {
            return context.GetMetadata(typeof(TAnalyzer));
        }
    }
}