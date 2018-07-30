using DotvvmAcademy.Validation.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Tests
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
            var factory = new MetadataNameFactory();

            var customer = factory.CreateTypeName("DataAccessLayer", "Customer");
            var product = factory.CreateTypeName("DataAccessLayer", "Product");
            var str = factory.CreateTypeName("System", "String");
            var list = factory.CreateTypeName("System.Collections.Generic", "List", 1);
            var listOfProducts = factory.CreateConstructedTypeName(list, ImmutableArray.Create(product));
            var entityCache = factory.CreateTypeName("DataAccessLayer", "EntityCache");
            var integer = factory.CreateTypeName("System", "Int32");
            var cartDto = factory.CreateTypeName("BusinessLayer", "CartDTO");
            var boughtProductHandler = factory.CreateNestedTypeName(customer, "ProductBoughtHandler");

            stringArrayType = factory.CreateArrayTypeName(str);
            dtoType = factory.CreateTypeName("BusinessLayer", "DTO");
            iProviderType = factory.CreateTypeName("BusinessLayer", "IProvider", 1);
            keyType = factory.CreateNestedTypeName(dtoType, "Key");
            tEntityType = factory.CreateTypeParameterName(entityCache, "TEntity");
            iProviderConstructed = factory.CreateConstructedTypeName(iProviderType, ImmutableArray.Create(cartDto));
            getKeyMethod = factory.CreateMethodName(dtoType, "GetKey", keyType);
            getMethod = factory.CreateMethodName(entityCache, "Get", tEntityType, parameters: ImmutableArray.Create(integer));
            boughtProductsProperty = factory.CreatePropertyName(customer, "BoughtProducts", listOfProducts);
            productBoughtEvent = factory.CreateEventName(customer, "ProductBought", boughtProductHandler);
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