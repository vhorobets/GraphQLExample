using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace WebApi.GraphQL.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(StoreDbContext dbContext, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(o => o.OrderId);
            Field(o => o.OrderDate);

            // simple solution
            //Field<ListGraphType<ProductType>>("products1", resolve: ctx => ctx.Source.Products.Select(p => p.Product));
            //Field<CustomerType>("customer", resolve: ctx => ctx.Source.Customer);

            // solution with data loader
            Func<IEnumerable<int>, Task<ILookup<int, Product>>> getProductsFunc = async (ids) =>
            {
                // get all products for orderIds grouped by orderId
                var data = await dbContext.Orders
                        .Where(o => ids.Contains(o.OrderId))
                        .Select(o => o.Products)
                        .SelectMany(c => c.Select(p => new { p.OrderId, p.Product }))
                        .GroupBy(x => x.OrderId)
                        .ToDictionaryAsync(gr => gr.Key, gr => gr.Select(x => x.Product).ToList());

                var list = new List<Tuple<int, Product>>();
                foreach (var item in data)
                {
                    list.AddRange(item.Value.Select(x => new Tuple<int, Product>(item.Key, x)));
                }
                
                return await Task.FromResult(list.ToLookup(x => x.Item1, x => x.Item2));
            };

            Func<IEnumerable<int>, Task<IDictionary<int, Customer>>> getCustomersFunc = async (ids) =>
            {
                return await dbContext.Customers.Where(c => ids.Contains(c.CustomerId)).ToDictionaryAsync(c => c.CustomerId, c => c);
            };

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: ctx =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Product>(
                        "GetProductsByOrderId", getProductsFunc
                    );

                    return loader.LoadAsync(ctx.Source.OrderId);
                }
            );

            Field<CustomerType>(
                "customer", 
                resolve: ctx =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddBatchLoader<int, Customer>(
                            "GetCustomersByCustomerId", getCustomersFunc
                        );
                    return loader.LoadAsync(ctx.Source.CustomerId);
                }
            );
        }
    }
}
