using DotvvmAcademy.Meta;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            return unit.Provider.GetRequiredService<IMemberInfoNameBuilder>().Build(type).ToString();
        }
    }
}