using System;

namespace DotvvmAcademy.Validation.Dothtml
{
    [Flags]
    public enum AllowedBinding
    {
        None = 0,
        Value = 1 << 0,
        Command = 1 << 1,
        StaticCommand = 1 << 2,
        Resource = 1 << 3,
        ControlProperty = 1 << 4,
        ControlCommand = 1 << 5
    }
}