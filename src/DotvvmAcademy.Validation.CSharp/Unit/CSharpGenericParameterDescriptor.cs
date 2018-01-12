using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpGenericParameterDescriptor
    {
        public CSharpGenericParameterDescriptor(string name)
        {
            Name = name;
        }

        public CSharpParameterConstraint Constraint { get; set; }

        public string Name { get; }

        public IEnumerable<CSharpTypeDescriptor> TypeConstraints { get; set; }

        public CSharpVarianceModifier VarianceModifier { get; set; }
    }
}