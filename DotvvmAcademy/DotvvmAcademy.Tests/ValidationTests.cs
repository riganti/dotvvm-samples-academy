using DotvvmAcademy.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Tests
{
    [TestClass]
    public class ValidationTests
    {

        [TestMethod]
        public void CodeStep_IllegalMethods_StaticMethod()
        {
            var step = new CodeStep()
            {
                Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            int.Parse(""a"");
        }
    }
}",
                ValidationFunction = (_, __, ___, ____) => { }
            };

            Assert.AreEqual(string.Format(Texts.IllegalMethodCall, "int.Parse"), step.ErrorMessage);
        }

        [TestMethod]
        public void CodeStep_IllegalMethods_InstanceMethod()
        {
            var step = new CodeStep()
            {
                Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            var a = Number1.ToString();
        }
    }
}",
                ValidationFunction = (_, __, ___, ____) => { }
            };

            Assert.AreEqual(string.Format(Texts.IllegalMethodCall, "int.ToString"), step.ErrorMessage);
        }


        [TestMethod]
        public void CodeStep_IllegalMethods_Lambda()
        {
            var step = new CodeStep()
            {
                Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            Func<int> a = () => 5;
            a();
        }
    }
}",
                ValidationFunction = (_, __, ___, ____) => { }
            };

            Assert.AreEqual(string.Format(Texts.IllegalMethodCall, "System.Func<int>.Invoke"), step.ErrorMessage);
        }

        [TestMethod]
        public void CodeStep_IllegalConstructor()
        {
            var step = new CodeStep()
            {
                Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            var str = new string(' ', 2);
        }
    }
}",
                ValidationFunction = (_, __, ___, ____) => { }
            };

            Assert.AreEqual(string.Format(Texts.IllegalConstructorCall, "string"), step.ErrorMessage);
        }

    }
}
