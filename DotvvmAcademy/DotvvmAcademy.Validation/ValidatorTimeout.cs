
namespace DotvvmAcademy.Validation.Cli
{
    public class ValidatorTimeout
    {
        public ValidatorTimeout(int timeout)
        {
            Timeout = timeout;
        }

        public int Timeout { get; }
    }
}
