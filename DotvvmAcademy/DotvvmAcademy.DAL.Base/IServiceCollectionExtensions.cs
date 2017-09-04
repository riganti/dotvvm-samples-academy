﻿using DotvvmAcademy.DAL.Base.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.DAL.Base
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDALBase(this IServiceCollection services)
        {
            services.AddSingleton<DotvvmAcademyContext>();
            services.AddSingleton<ILessonDeserializer, JsonLessonDeserializer>();
        }
    }
}