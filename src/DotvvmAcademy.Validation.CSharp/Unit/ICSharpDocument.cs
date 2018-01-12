﻿using DotvvmAcademy.Validation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# document.
    /// </summary>
    public interface ICSharpDocument : IDocument
    {
        ICSharpNamespace GetGlobalNamespace();

        ICSharpNamespace GetNamespace(string name);

        void Remove<TCSharpObject>(TCSharpObject csharpObject)
            where TCSharpObject : ICSharpObject;
    }
}