using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<Func<string, Service>>(s=>new Service(s));
            var provider = collection.BuildServiceProvider();
            var result = provider.GetService<IEnumerable<Service>>();
        }

        public class Service
        {
            public Service(string s)
            {
                S = s;
            }

            public string S { get; }

            public string Do()
            {
                return S;
            }
        }
    }
}
