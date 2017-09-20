using System;

namespace DotvvmAcademy.Lessons.BasicMvvm.ViewModels
{
    public class CalculatorViewModel
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