using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Validation
{
    public class ActivatableObject
    {
        public bool IsActive { get; protected set; } = true;
    }
}
