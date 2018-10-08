using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Meta
{
    public interface IMetaConverter<TSource, TTarget>
    {
        IEnumerable<TTarget> Convert(TSource source);
    }
}