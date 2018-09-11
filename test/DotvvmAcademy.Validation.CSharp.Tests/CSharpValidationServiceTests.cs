using DotvvmAcademy.Validation.CSharp.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class CSharpValidationServiceTests
    {
        [Fact]
        public async Task CSharpValidation_DefaultService_CreateErrors()
        {
            var provider = GetServiceProvider();
            var unit = new CSharpUnit(provider);
            unit.GetMethod("Test::Off");
            var service = new CSharpValidationService();
            var source = new CSharpSourceCode("public class Test {}", "Test.cs", true);
            var diagnostics = await service.Validate(unit, ImmutableArray.Create<ISourceCode>(source));
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddMetaScopeFriendly();
            return c.BuildServiceProvider();
        }
    }
}
