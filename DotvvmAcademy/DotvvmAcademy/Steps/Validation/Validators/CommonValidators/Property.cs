using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public class Property
    {
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public string Type { get; }
    }
}
