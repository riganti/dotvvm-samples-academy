using AutoMapper;
using System.Collections.Generic;

namespace DotvvmAcademy.Services
{
    public class AutoMapperInitializer
    {
        private readonly IEnumerable<Profile> profiles;

        public AutoMapperInitializer(IEnumerable<Profile> profiles)
        {
            this.profiles = profiles;
        }

        public void Initialize()
        {
            Mapper.Initialize(config =>
            {
                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }
            });
        }
    }
}