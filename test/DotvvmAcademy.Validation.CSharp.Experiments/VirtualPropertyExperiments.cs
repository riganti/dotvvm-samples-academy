using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class VirtualPropertyExperiments
    {
        public class X1
        {
            public string Property1 { get; set; }

            public int? Property2 { get; set; }
        }

        public abstract class X2
        {
            public string Property3 { get; set; }

            public int? Property4 { get; set; }
        }

        public abstract class X3 : X2
        {

        }

        [Fact]
        public void X1_SimpleClass_NotVirtual()
        {
            Assert.False(typeof(X1).GetProperty(nameof(X1.Property1)).GetMethod.IsVirtual);
            Assert.False(typeof(X1).GetProperty(nameof(X1.Property2)).GetMethod.IsVirtual);
        }

        [Fact]
        public void X2X3_InheritsAbstract_NotVirtual()
        {
            Assert.False(typeof(X3).GetProperty(nameof(X2.Property3)).GetMethod.IsVirtual);
            Assert.False(typeof(X3).GetProperty(nameof(X2.Property4)).GetMethod.IsVirtual);
        }
    }
}
