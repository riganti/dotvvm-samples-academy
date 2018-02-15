#define SAMPLEDIRECTIVE

using System;
using System.Collections.Generic;
using System.Linq;

public interface IEntity
{
    int ID { get; set; }
}

namespace DataAccessLayer
{
    public class EntityCache
    {
        public TEntity Get<TEntity>(int id)
            where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public void Cache<TEntity>(TEntity entity)
            where TEntity : IEntity
        {
            throw new NotImplementedException();
        }
    }

    public class Customer : IEntity
    {
        public int ID { get; set; }

        public List<Product> BoughtProducts { get; set; }

        public delegate void ProductBoughtHandler(Product product);

        public event ProductBoughtHandler ProductBought;
    }

    public class Cart : IEntity
    {
        private List<Product> products = new List<Product>();

        public int ID { get; set; }

        public Product this[int index]
        {
            get { return products[index]; }
            set { products[index] = value; }
        }
    }

    public enum ProductKind
    {
        Consumable,
        Vehicle,
        Building
    }

    public class Product : IEntity
    {
        public int ID { get; set; }

        public string[] Manufacturers { get; set; }

        public ProductKind Kind { get; set; }
    }
}

namespace BusinessLayer
{
    using DataAccessLayer;

    public abstract class DTO
    {
        public int ID { get; set; }

        public Key GetKey()
        {
            return new Key(this);
        }

        public static implicit operator DTO(Customer customer)
        {
            return new CustomerDTO { ID = customer.ID };
        }

        public static explicit operator Customer(DTO dto)
        {
            return new Customer { ID = dto.ID };
        }

        public static implicit operator DTO(Product product)
        {
            return new ProductDTO { ID = product.ID };
        }

        public static explicit operator Product(DTO dto)
        {
            return new Product { ID = dto.ID };
        }

        public static implicit operator DTO(Cart cart)
        {
            return new CartDTO { ID = cart.ID };
        }

        public static explicit operator Cart(DTO dto)
        {
            return new Cart { ID = dto.ID };
        }

        public struct Key : IEquatable<Key>
        {
            internal Key(DTO dto)
            {
                Dto = dto;
            }

            public DTO Dto { get; }

            public bool Equals(Key other)
            {
                return ReferenceEquals(this, other);
            }
        }
    }

    public class CustomerDTO : DTO
    {
    }

    public class ProductDTO : DTO
    {
    }

    public class CartDTO : DTO
    {
    }

    public interface IProvider<out TDTO>
        where TDTO : DTO
    {
        TDTO Get(int id);

        TDTO[] Get();
    }

    public class Provider<TDTO> : IProvider<TDTO>
        where TDTO : DTO
    {
        public static Provider<DTO> Default;

        static Provider()
        {
            Default = new Provider<DTO>();
        }

        public TDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public TDTO[] Get()
        {
            throw new NotImplementedException();
        }

        ~Provider()
        {
            FreeResources();
        }

        private void FreeResources()
        {
            throw new NotImplementedException();
        }
    }
}