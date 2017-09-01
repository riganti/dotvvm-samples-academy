using System;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson1ViewModel
    {
        public int Number1 { get; set; }

        public int Number2 { get; set; }

        public int Result { get; set; }

        public void Calculate()
        {
            Result = Number1 + Number2;
        }
    }
}