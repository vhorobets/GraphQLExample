using System;
using System.Collections.Generic;
using System.Linq;
using Persistence.Entities;

namespace Persistence
{
    public class StoreDbInitializer
    {
        private StoreDbContext _context;

        private IDictionary<int, Manufacturer> _manufactures = new Dictionary<int, Manufacturer>();
        private IDictionary<int, Product> _products = new Dictionary<int, Product>();
        private IDictionary<int, Customer> _customers = new Dictionary<int, Customer>();

        public StoreDbInitializer(StoreDbContext context)
        {
            _context = context;
        }

        public static void Initialize(StoreDbContext context)
        {
            var initializer = new StoreDbInitializer(context);
            initializer.Seed();
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (_context.Manufacturers.Any())
            {
                return; // Db has been seeded
            }

            SeedManufactures();

            SeedProducts();

            SeedCustomers();

            SeedOrders();
        }

        #region Private
        private void SeedManufactures()
        {
            _manufactures.Add(1, new Manufacturer() { Name = "Apple", Country = "USA" });
            _manufactures.Add(2, new Manufacturer() { Name = "Samsung", Country = "Korea" });
            _manufactures.Add(3, new Manufacturer() { Name = "LG", Country = "Korea" });
            _manufactures.Add(4, new Manufacturer() { Name = "Nokia", Country = "Finland" });
            _manufactures.Add(5, new Manufacturer() { Name = "Xiaomi", Country = "China" });

            _context.Manufacturers.AddRange(_manufactures.Values);

            _context.SaveChanges();
        }

        private void SeedProducts()
        {
            _products.Add(1, new Product() { Name = "iPhone X", Price = 1200m, Manufacturer = _manufactures[1] });
            _products.Add(2, new Product() { Name = "iPhone XR", Price = 850m, Manufacturer = _manufactures[1] });
            _products.Add(3, new Product() { Name = "Mac Book Pro", Price = 2500m, Manufacturer = _manufactures[1] });

            _products.Add(4, new Product() { Name = "Galaxy S9", Price = 650m, Manufacturer = _manufactures[2] });
            _products.Add(5, new Product() { Name = "Note 10", Price = 1450m, Manufacturer = _manufactures[2] });

            _products.Add(6, new Product() { Name = "G7", Price = 700m, Manufacturer = _manufactures[3] });

            _products.Add(7, new Product() { Name = "Lumia 620", Price = 250m, Manufacturer = _manufactures[4] });

            _products.Add(8, new Product() { Name = "Readmi 6", Price = 150m, Manufacturer = _manufactures[5] });

            _context.Products.AddRange(_products.Values);

            _context.SaveChanges();
        }

        private void SeedCustomers()
        {
            _customers.Add(1, new Customer() { FirstName = "Oleg", LastName = "Savchuk", Age = 27, CustomerType = CustomerTypeEnum.Gold });
            _customers.Add(2, new Customer() { FirstName = "Vasia", LastName = "Pupkin", Age = 30, CustomerType = CustomerTypeEnum.Premium });
            _customers.Add(3, new Customer() { FirstName = "Olena", LastName = "Ivanova", Age = 20, CustomerType = CustomerTypeEnum.Gold });
            _customers.Add(4, new Customer() { FirstName = "Ivan", LastName = "Petrov", Age = 47 });
            _customers.Add(5, new Customer() { FirstName = "Andrii", LastName = "Ivanuyk", Age = 29 });
            _customers.Add(6, new Customer() { FirstName = "Alex", LastName = "Singer" });

            _context.Customers.AddRange(_customers.Values);

            _context.SaveChanges();
        }

        private void SeedOrders()
        {
            var orders = new List<Order>();

            var start = DateTime.Now.AddDays(-10);
            Random random = new Random();

            var orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[1] }, new OrderProduct() { Product = _products[3] }, new OrderProduct() { Product = _products[5] } };
            orders.Add(new Order() { Customer = _customers[1], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[2] }, new OrderProduct() { Product = _products[4] }, new OrderProduct() { Product = _products[3] }, new OrderProduct() { Product = _products[7] } };
            orders.Add(new Order() { Customer = _customers[1], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[2] }, new OrderProduct() { Product = _products[4] } };
            orders.Add(new Order() { Customer = _customers[3], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[6] }, new OrderProduct() { Product = _products[6] } };
            orders.Add(new Order() { Customer = _customers[5], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[5] }, new OrderProduct() { Product = _products[1] } };
            orders.Add(new Order() { Customer = _customers[5], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[8] }, new OrderProduct() { Product = _products[6] } };
            orders.Add(new Order() { Customer = _customers[2], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            orderProducts = new List<OrderProduct>() { new OrderProduct() { Product = _products[7] }, new OrderProduct() { Product = _products[2] }, new OrderProduct() { Product = _products[4] } };
            orders.Add(new Order() { Customer = _customers[4], OrderDate = start.AddMilliseconds(random.Next()), Products = orderProducts });

            _context.Orders.AddRange(orders);

            _context.SaveChanges();
        }
        #endregion
    }
}
