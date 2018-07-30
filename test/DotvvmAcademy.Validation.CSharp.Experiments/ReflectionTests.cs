using System.Reflection;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void IDKTest()
        {
            var test1 = Type.GetType("Markdig.Syntax.HeadingBlock, Markdig");
            var test2 = Type.GetType("System.String, mscorlib");
            var test3 = Type.GetType("Mono.Cecil.FieldDefinition,Mono.Cecil");
            var test4 = Type.GetType("System.Object&");
            var test5 = typeof(One<int, int>.Two<int, int>);
            var test6 = Type.GetType("DotvvmAcademy.Validation.CSharp.Experiments.ReflectionTests+One`2+Two`2[System.Int32, System.Int32, System.Int32]");
        }

        public class One<T1, T2>
        {

            public class Two<T3,T4>
            {

            }
        }
    }
}   