using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.xUnitTests
{
    public class XmlParserTests
    {


        [Theory]
        [InlineData(4)]
        [InlineData(2)]
        public void PassingTest(int expectedResult)
        {
            Assert.Equal(4, expectedResult);
        }

    }
}
