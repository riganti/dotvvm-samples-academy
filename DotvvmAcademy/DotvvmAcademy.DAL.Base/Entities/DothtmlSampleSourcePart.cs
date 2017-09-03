using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class DothtmlSampleSourcePart : BasicSampleSourcePart
    {
        public Sample ViewModel { get; set; }

        public Sample MasterPage { get; set; }
    }
}
