using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    public interface IMetaConverter<TFrom, TTo>
    {
        TTo Convert(TFrom meta);
    }
}