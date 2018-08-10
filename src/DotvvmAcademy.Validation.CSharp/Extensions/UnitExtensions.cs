using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        internal static string GetMetaName<TType>(this IUnit unit)
        {
            return unit.GetMetaName(typeof(TType));
        }

        internal static string GetMetaName(this IUnit unit, Type type)
        {
            // TODO: Replace Type.FullName with a service
            return type.FullName;
        }
    }
}