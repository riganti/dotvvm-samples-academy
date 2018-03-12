using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public class ValidationPipelineBuilder : IValidationPipelineBuilder
    {
        private readonly IList<Func<ValidationDelegate, ValidationDelegate>> middlewares
            = new List<Func<ValidationDelegate, ValidationDelegate>>();

        public ValidationDelegate Build()
        {
            ValidationDelegate service = delegate
            {
                return Task.CompletedTask;
            };

            foreach(var middleware in middlewares.Reverse())
            {
                service = middleware(service);
            }

            return service;
        }

        public IValidationPipelineBuilder Use(Func<ValidationDelegate, ValidationDelegate> middleware)
        {
            middlewares.Add(middleware);
            return this;
        }
    }
}
