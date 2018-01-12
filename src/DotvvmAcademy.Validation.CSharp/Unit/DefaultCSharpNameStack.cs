using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpNameStack : ICSharpNameStack
    {
        private readonly Stack<string> stack = new Stack<string>();

        public string PopName() => stack.Pop();

        public void PushName(string name) => stack.Push(name);
    }
}