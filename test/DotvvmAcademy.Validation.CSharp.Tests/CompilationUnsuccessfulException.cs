using Microsoft.CodeAnalysis.Emit;
using System;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [Serializable]
    public class CompilationUnsuccessfulException : Exception
    {
        public CompilationUnsuccessfulException(string message, EmitResult emitResult) : base(message)
        {
            EmitResult = emitResult;
        }

        public CompilationUnsuccessfulException(
            string message,
            EmitResult emitResult,
            Exception inner)
            : base(message, inner)
        {
            EmitResult = emitResult;
        }

        public EmitResult EmitResult { get; }
    }
}