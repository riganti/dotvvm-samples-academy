using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public delegate Task<IEnumerable<ValidationError>> ValidatorDelegate(string code, IEnumerable<string> dependencies = null);
}