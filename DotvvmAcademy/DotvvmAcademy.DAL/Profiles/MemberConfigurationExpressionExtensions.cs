using AutoMapper;
using DotvvmAcademy.DAL.Services;
using System;
using System.IO;

namespace DotvvmAcademy.DAL.Profiles
{
    public static class MemberConfigurationExpressionExtensions
    {
        public static void ResolveAsPath<TSource, TDestination>(this IMemberConfigurationExpression<TSource, TDestination, string> ex,
            Func<TSource, FileSystemInfo> infoAccessor,
            ContentDirectoryEnvironment env)
        {
            ex.ResolveUsing((s, d) =>
            {
                var info = infoAccessor(s);
                return env.GetRelative(info.FullName);
            });
        }
    }
}