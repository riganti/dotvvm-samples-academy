namespace DotvvmAcademy.Course.Calculator.ViewModels
{
    public class CalculatorViewModel
    {
        public int Number1 { get; set; }

        public int Number2 { get; set; }

        public int Result { get; set; }

        public void Add()
        {
            Result = Number1 + Number2;
        }

        public void Subtract()
        {
            Result = Number1 - Number2;
        }

        public void Multiply()
        {
            Result = Number1 * Number2;
        }

        public void Divide()
        {
            Result = Number1 / Number2;
        }
    }
}