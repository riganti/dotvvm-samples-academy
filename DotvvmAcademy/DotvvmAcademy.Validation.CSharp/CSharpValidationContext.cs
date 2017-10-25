using DotvvmAcademy.Validation.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationContext : IValidationContext<CSharpValidationRequest, CSharpValidationResponse>
    {
        public CSharpValidationContext(CSharpValidationRequest request, CSharpValidationResponse response, 
            CSharpResolvedValidationMethod method, ImmutableArray<CSharpSyntaxTree> validatedTrees)
        {
            Request = request;
            Response = response;
            Method = method;
            ValidatedTrees = validatedTrees;
        }

        public CSharpResolvedValidationMethod Method { get; }

        public CSharpValidationRequest Request { get; }

        public CSharpValidationResponse Response { get; }

        public ImmutableArray<CSharpSyntaxTree> ValidatedTrees { get; }
    }
}