﻿namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# class.
    /// </summary>
    public interface ICSharpClass : ICSharpConstructibleType, ICSharpAllowsInheritance, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsSealedModifier
    {
        bool HasDestructor { get; set; }
    }
}