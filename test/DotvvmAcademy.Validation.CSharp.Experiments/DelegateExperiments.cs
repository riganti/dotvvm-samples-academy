using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public delegate void AmazingDelegate(IAmazing a);

    public delegate void MoreAmazingDelegate(IMoreAmazing a);

    public interface IAmazing
    {
    }

    public interface IMoreAmazing : IAmazing
    {
    }

    public class DelegateExperiments
    {
        public void Test(IAmazing amazing)
        {
        }

        [Fact]
        public void CompatibilityExperiment()
        {
            AmazingDelegate a = Test;
            MoreAmazingDelegate m = new MoreAmazingDelegate(a);
        }
    }
}