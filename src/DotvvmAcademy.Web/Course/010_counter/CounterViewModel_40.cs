namespace DotvvmAcademy.Course.Counter
{
    public class CounterViewModel
    {
        public int Difference { get; set; }

        public int Result { get; set; }

        public void Increment()
        {
            Result += Difference;
        }

        public void Decrement()
        {
            Result -= Difference;
        }
    }
}
