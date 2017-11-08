using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpNameStack : ICSharpNameStack
    {
        private readonly Stack<string> stack = new Stack<string>();

        public string PopName() => stack.Pop();

        public void PushName(string name) => stack.Push(name);
    }
}