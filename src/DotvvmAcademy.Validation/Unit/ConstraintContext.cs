using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public class ConstraintContext
    {
        public ConstraintContext(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IServiceProvider Provider { get; }
    }
}