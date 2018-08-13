using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course.Calculator
{
    public class CalculatorViewModel
    {
        public int LeftOperand { get; set; }

        public int Result { get; set; }

        public int RightOperand { get; set; }

        public void Add()
        {
            Result = LeftOperand + RightOperand;
        }

        public void Divide()
        {
            Result = LeftOperand / RightOperand;
        }

        public void Multiply()
        {
            Result = LeftOperand * RightOperand;
        }

        public void Subtract()
        {
            Result = LeftOperand - RightOperand;
        }
    }
}