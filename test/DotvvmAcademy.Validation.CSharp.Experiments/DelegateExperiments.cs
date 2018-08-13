using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    [TestClass]
    public class DelegateExperiments
    {
        [TestMethod]
        public void CompatibilityExperiment()
        {
            AmazingDelegate a = null;
            MoreAmazingDelegate m = new MoreAmazingDelegate(a);
        }
    }
}