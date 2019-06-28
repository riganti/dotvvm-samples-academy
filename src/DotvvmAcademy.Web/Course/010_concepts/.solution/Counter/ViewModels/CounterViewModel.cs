namespace DotvvmAcademy.Course.Counter
{
    public class CounterViewModel
    {
        public int Difference { get; set; }

        public int Result { get; set; }

        public void Add()
        {
            Result += Difference;
        }

        public void Subtract()
        {
            Result -= Difference;
        }
    }
}
