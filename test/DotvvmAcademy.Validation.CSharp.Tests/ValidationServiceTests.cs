using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class ValidationServiceTests
    {
        public const string BasicSource = @"
public abstract class Vehicle
{
    int Location { get; set; }

    void Move();
}

public class Car : Vehicle
{
    public void Move()
    {
        Location++;
    };
}
";

        [TestMethod]
        public void BasicTestValidationServiceTest()
        {
            var service = new TestValidationService();
            var context = new ValidationContext();
            context.SetItem(CSharpCompilationMiddleware.SourcesKey, ImmutableArray.Create(BasicSource));
            context.SetItem(CSharpCompilationMiddleware.OptionsKey, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var metadata = new MetadataCollection();
        }
    }
}
