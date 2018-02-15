using DotvvmAcademy.Validation.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using static DotvvmAcademy.Validation.CSharp.MetadataName;

namespace DotvvmAcademy.Validation.Tests
{
    [TestClass]
    public class MetadataNameTests
    {
        private MetadataName boughtProductsProperty;
        private MetadataName dtoType;
        private MetadataName getMethod;
        private MetadataName getKeyMethod;
        private MetadataName iProviderConstructed;
        private MetadataName iProviderType;
        private MetadataName keyType;
        private MetadataName productBoughtEvent;
        private MetadataName stringArrayType;
        private MetadataName tEntityType;

        public MetadataNameTests()
        {
            var customer = CreateTypeName("DataAccessLayer", "Customer");
            var product = CreateTypeName("DataAccessLayer", "Product");
            var str = CreateTypeName("System", "String");
            var list = CreateTypeName("System.Collections.Generic", "List", 1);
            var listOfProducts = CreateConstructedTypeName(list, ImmutableArray.Create(product));
            var entityCache = CreateTypeName("DataAccessLayer", "EntityCache");
            var integer = CreateTypeName("System", "Int32");
            var cartDto = CreateTypeName("BusinessLayer", "CartDTO");
            var boughtProductHandler = CreateNestedTypeName(customer, "ProductBoughtHandler");

            stringArrayType = CreateArrayTypeName(str);
            dtoType = CreateTypeName("BusinessLayer", "DTO");
            iProviderType = CreateTypeName("BusinessLayer", "IProvider", 1);
            keyType = CreateNestedTypeName(dtoType, "Key");
            tEntityType = CreateGenericParameterTypeName("TEntity");
            iProviderConstructed = CreateConstructedTypeName(iProviderType, ImmutableArray.Create(cartDto));
            getKeyMethod = CreateMethodName(dtoType, "GetKey", keyType);
            getMethod = CreateMethodName(entityCache, "Get", tEntityType, ImmutableArray.Create(tEntityType), ImmutableArray.Create(integer));
            boughtProductsProperty = CreatePropertyName(listOfProducts, customer, "BoughtProducts");
            productBoughtEvent = CreateFieldOrEventName(boughtProductHandler, customer, "ProductBought");
        }

        [TestMethod]
        public void ArrayTypeTest()
        {
            const string expected = "System.String[]";

            Assert.AreEqual(expected, stringArrayType.ToString());
            Assert.AreEqual(expected, stringArrayType.FullName);
            Assert.AreEqual(expected, stringArrayType.ReflectionName);
        }

        [TestMethod]
        public void EventFieldTest()
        {
            const string cecil = "DataAccessLayer.Customer/ProductBoughtHandler DataAccessLayer.Customer::ProductBought";
            const string reflection = "ProductBought";

            Assert.AreEqual(cecil, productBoughtEvent.ToString());
            Assert.AreEqual(cecil, productBoughtEvent.FullName);
            Assert.AreEqual(reflection, productBoughtEvent.ReflectionName);
        }

        [TestMethod]
        public void GenericInstanceTest()
        {
            const string cecil = "BusinessLayer.IProvider`1<BusinessLayer.CartDTO>";
            const string reflection = "BusinessLayer.IProvider`1[BusinessLayer.CartDTO]";

            Assert.AreEqual(cecil, iProviderConstructed.ToString());
            Assert.AreEqual(cecil, iProviderConstructed.FullName);
            Assert.AreEqual(reflection, iProviderConstructed.ReflectionName);
        }

        [TestMethod]
        public void GenericMethodTest()
        {
            const string cecil = "TEntity DataAccessLayer.EntityCache::Get<TEntity>(System.Int32)";
            const string reflection = "Get";

            Assert.AreEqual(cecil, getMethod.ToString());
            Assert.AreEqual(cecil, getMethod.FullName);
            Assert.AreEqual(reflection, getMethod.ReflectionName);
        }

        [TestMethod]
        public void GenericParameterTest()
        {
            const string expected = "TEntity";

            Assert.AreEqual(expected, tEntityType.ToString());
            Assert.AreEqual(expected, tEntityType.FullName);
            Assert.AreEqual(expected, tEntityType.ReflectionName);
        }

        [TestMethod]
        public void GenericTopLevelTypeTest()
        {
            const string expected = "BusinessLayer.IProvider`1";

            Assert.AreEqual(expected, iProviderType.ToString());
            Assert.AreEqual(expected, iProviderType.FullName);
            Assert.AreEqual(expected, iProviderType.ReflectionName);
        }

        [TestMethod]
        public void MethodTest()
        {
            const string cecil = "BusinessLayer.DTO/Key BusinessLayer.DTO::GetKey()";
            const string reflection = "GetKey";

            Assert.AreEqual(cecil, getKeyMethod.ToString());
            Assert.AreEqual(cecil, getKeyMethod.FullName);
            Assert.AreEqual(reflection, getKeyMethod.ReflectionName);
        }

        [TestMethod]
        public void NestedTypeTest()
        {
            const string cecil = "BusinessLayer.DTO/Key";
            const string reflection = "BusinessLayer.DTO+Key";

            Assert.AreEqual(cecil, keyType.ToString());
            Assert.AreEqual(cecil, keyType.FullName);
            Assert.AreEqual(reflection, keyType.ReflectionName);
        }

        [TestMethod]
        public void PropertyTest()
        {
            const string cecil = "System.Collections.Generic.List`1<DataAccessLayer.Product> DataAccessLayer.Customer::BoughtProducts()";
            const string reflection = "BoughtProducts";

            Assert.AreEqual(cecil, boughtProductsProperty.ToString());
            Assert.AreEqual(cecil, boughtProductsProperty.FullName);
            Assert.AreEqual(reflection, boughtProductsProperty.ReflectionName);
        }

        [TestMethod]
        public void TopLevelTypeTest()
        {
            const string expected = "BusinessLayer.DTO";

            Assert.AreEqual(expected, dtoType.ToString());
            Assert.AreEqual(expected, dtoType.FullName);
            Assert.AreEqual(expected, dtoType.ReflectionName);
        }
    }
}