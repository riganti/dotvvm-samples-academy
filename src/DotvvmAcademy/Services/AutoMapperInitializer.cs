using AutoMapper;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Services
{
    public class AutoMapperInitializer
    {
        private readonly IEnumerable<Profile> profiles;
        private readonly IServiceProvider serviceProvider;

        public AutoMapperInitializer(IEnumerable<Profile> profiles, IServiceProvider serviceProvider)
        {
            this.profiles = profiles;
            this.serviceProvider = serviceProvider;
        }

        public void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.ConstructServicesUsing(serviceProvider.GetService);

                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }
            });
        }
    }
}