﻿using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class ValidationUnit : IValidationUnit
    {
        public ValidationUnit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public ICollection<IConstraint> Constraints { get; } = new HashSet<IConstraint>(new ConstraintEqualityComparer());

        public IServiceProvider Provider { get; }
    }
}