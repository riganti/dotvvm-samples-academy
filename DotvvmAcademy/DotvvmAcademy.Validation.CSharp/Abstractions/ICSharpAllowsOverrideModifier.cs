﻿namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that can override its base.
    /// </summary>
    public interface ICSharpAllowsOverrideModifier
    {
        bool IsOverriding { get; set; }
    }
}