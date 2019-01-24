using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace ConsoleApp
{
    class Program
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Store;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .Options;

            var context = new StoreDbContext(options);

            context.Database.EnsureDeleted();
            StoreDbInitializer.Initialize(context);


            var m1 = context.Manufacturers.Find(1);
            var m2 = context.Manufacturers.FirstAsync(x => x.ManufacturerId == 2).Result;

            var d3 = context.Orders.Where(x => x.OrderId == 2).Select(x => x.Customer.FirstName);

            var allOrders = context.Orders.ToListAsync().Result;


            Console.ReadKey();

           
        }
    }
}
