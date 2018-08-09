using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public interface IAmazing
    {
    }

    public interface IMoreAmazing : IAmazing
    {
    }

    public delegate void AmazingDelegate(IAmazing a);

    public delegate void MoreAmazingDelegate(IMoreAmazing a);

    public class DelegateCompatibilityExperiment
    {
        public void Experiment()
        {
            AmazingDelegate a = null;
            MoreAmazingDelegate m = new MoreAmazingDelegate(a);
        }
    }
}
