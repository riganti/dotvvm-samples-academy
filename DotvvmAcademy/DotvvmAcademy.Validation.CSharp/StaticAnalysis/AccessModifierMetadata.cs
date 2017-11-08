using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class AccessModifierMetadata : IStaticAnalysisMetadata
    {
        public Accessibility Accessibility { get; set; }
    }
}