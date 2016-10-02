using DotvvmAcademy.Lessons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Tests
{
    [TestClass]
    public class Lesson1Tests
    {
        private DothtmlStep basicControlsStep;
        private CodeStep viewModelPropertiesStep;
        private CodeStep viewModelCommandStep;
        private DothtmlStep textBoxBindingsStep;
        private DothtmlStep buttonBindingStep;


        [TestInitialize]
        public void Init()
        {
            var lesson = new Lesson1();

            basicControlsStep = lesson.Step2;
            viewModelPropertiesStep = lesson.Step3;
            viewModelCommandStep = lesson.Step4;
            textBoxBindingsStep = lesson.Step5;
            buttonBindingStep = lesson.Step6;
        }



        [TestMethod]
        public void Lesson1_BasicControls_EmptyCode()
        {
            basicControlsStep.Code = "";

            Assert.AreEqual(Lesson1Texts.ThreeTextBoxControlsError, basicControlsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_BasicControls_OneTextBoxOnly()
        {
            basicControlsStep.Code = " <dot:TextBox />";

            Assert.AreEqual(Lesson1Texts.ThreeTextBoxControlsError, basicControlsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_BasicControls_ThreeTextBoxes_NoButton()
        {
            basicControlsStep.Code = " <dot:TextBox /> <dot:TextBox /> <dot:TextBox />";

            Assert.AreEqual(Lesson1Texts.OneButtonControlError, basicControlsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_BasicControls_Correct()
        {
            basicControlsStep.Code = " <dot:TextBox /> <dot:TextBox /> <dot:TextBox /> <dot:Button />";

            Assert.AreEqual("", basicControlsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_BasicControls_MalformedHtml()
        {
            basicControlsStep.Code = " <dot:TextBox> <dot:TextBox /> <dot:TextBox /> <dot:Button />";

            Assert.AreEqual(Lesson1Texts.ThreeTextBoxControlsError, basicControlsStep.ErrorMessage);
        }


        [TestMethod]
        public void Lesson1_ViewModelProperties_None()
        {
            viewModelPropertiesStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
    }
}";

            Assert.AreEqual(string.Format(Lesson1Texts.PropertyNotFound, "Number1"), viewModelPropertiesStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ViewModelProperties_InvalidType()
        {
            viewModelPropertiesStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public Int32 Number1 { get; set; }
        public System.Int32 Number2 { get; set; }
        public string Result { get; set; }
    }
}";

            Assert.AreEqual(string.Format(Lesson1Texts.PropertyNotFound, "Result"), viewModelPropertiesStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ViewModelProperties_Correct()
        {
            viewModelPropertiesStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public Int32 Number1 { get; set; }
        public System.Int32 Number2 { get; set; }
        public int Result { get; set; }
    }
}";

            Assert.AreEqual("", viewModelPropertiesStep.ErrorMessage);
        }


        [TestMethod]
        public void Lesson1_ViewModelCommand_None()
        {
            viewModelCommandStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }
}";

            Assert.AreEqual(Lesson1Texts.CommandNotFound, viewModelCommandStep.ErrorMessage);
        }


        [TestMethod]
        public void Lesson1_ViewModelCommand_InvalidSignature()
        {
            viewModelCommandStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate(int a) 
        {
        }
    }
}";

            Assert.AreEqual(Lesson1Texts.CommandSignatureError, viewModelCommandStep.ErrorMessage);
        }


        [TestMethod]
        public void Lesson1_ViewModelCommand_InvalidImplementation()
        {
            viewModelCommandStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
        }
    }
}";

            Assert.AreEqual(Lesson1Texts.CommandResultError, viewModelCommandStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ViewModelCommand_InvalidImplementation_Throws()
        {
            viewModelCommandStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            var a = ""aaa""[4];
        }
    }
}";

            Assert.AreEqual(Lesson1Texts.CommandResultError, viewModelCommandStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ViewModelCommand_Correct()
        {
            viewModelCommandStep.Code = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            var sum = Number1 + Number2;
            Result = sum;
        }
    }
}";

            Assert.AreEqual("", viewModelCommandStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_TextBoxBindings_InvalidProperties()
        {
            textBoxBindingsStep.Code = @"<dot:TextBox Text=""{value:  Number1}"" />
<dot:TextBox  Text=""{{value: Number1 }}""   />
<dot:TextBox anything=""T"" Text=""{value:Result}"" />
<dot:Button />";

            Assert.AreEqual(Lesson1Texts.TextBoxBindingsError, textBoxBindingsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_TextBoxBindings_Correct()
        {
            textBoxBindingsStep.Code = @"<dot:TextBox Text=""{value:  Number1}"" />
<dot:TextBox  Text=""{{value: Number2 }}""   />
<dot:TextBox anything=""T"" Text=""{value:Result}"" />
<dot:Button />";

            Assert.AreEqual("", textBoxBindingsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_TextBoxBindings_Missing()
        {
            textBoxBindingsStep.Code = @"<dot:TextBox></dot:TextBox>
<dot:TextBox  Text=""{{value: Number2 }}""   />
<dot:TextBox anything=""T"" Text=""{value:Result}"" />
<dot:Button />";

            Assert.AreEqual(string.Format(Texts.MissingPropertyError, "TextBox", "Text"), textBoxBindingsStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_TextBoxBindings_InvalidBindingType()
        {
            textBoxBindingsStep.Code = @"<dot:TextBox Text=""{value:  Number1}"" />
<dot:TextBox  Text=""{{command: Number2 }}""   />
<dot:TextBox anything=""T"" Text=""{value:Result}"" />
<dot:Button />";

            Assert.AreEqual(string.Format(Texts.ValueBindingExpected, "TextBox", "Text"), textBoxBindingsStep.ErrorMessage);
        }


        [TestMethod]
        public void Lesson1_ButtonBinding_None()
        {
            buttonBindingStep.Code = @"<dot:TextBox Text=""{value: Number1}"" />
<dot:TextBox Text=""{{value: Number2 }}""   />
<dot:TextBox Text=""{value:Result}"" />
<dot:Button />";

            Assert.AreEqual(string.Format(Texts.MissingPropertyError, "Button", "Click"), buttonBindingStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ButtonBinding_MissingText()
        {
            buttonBindingStep.Code = @"<dot:TextBox Text=""{value: Number1}"" />
<dot:TextBox Text=""{{value: Number2 }}""   />
<dot:TextBox Text=""{value:Result}"" />
<dot:Button Click=""{command: Calculate()}"" />";

            Assert.AreEqual(string.Format(Texts.MissingPropertyError, "Button", "Text"), buttonBindingStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ButtonBinding_ValueBinding()
        {
            buttonBindingStep.Code = @"<dot:TextBox Text=""{value: Number1}"" />
<dot:TextBox Text=""{{value: Number2 }}""   />
<dot:TextBox Text=""{value:Result}"" />
<dot:Button Click=""{value: Calculate()}"" />";

            Assert.AreEqual(string.Format(Texts.CommandBindingExpected, "Button", "Click"), buttonBindingStep.ErrorMessage);
        }

        [TestMethod]
        public void Lesson1_ButtonBinding_Correct()
        {
            buttonBindingStep.Code = @"<dot:TextBox Text=""{value: Number1}"" />
<dot:TextBox Text=""{{value: Number2 }}""   />
<dot:TextBox Text=""{value:Result}"" />
<dot:Button Click=""{command: Calculate()}"" Text=""Calculate!"" />";

            Assert.AreEqual("", buttonBindingStep.ErrorMessage);
        }

    }
}
