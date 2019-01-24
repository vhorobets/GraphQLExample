using System.Linq;
using GraphQL.Types;
using Persistence;
using WebApi.GraphQL.Types;

namespace WebApi.GraphQL
{
    public class StoreQuery : ObjectGraphType // GraphQL possible queries
    {
        public StoreQuery(StoreDbContext dbContext)
        {
            Field<ListGraphType<CustomerType>>(
                "customers",
                resolve: ctx => dbContext.Customers.ToList() // could be a data or unawaited task
            );

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: ctx => dbContext.Products.ToList()
            );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: ctx => dbContext.Products.Find(ctx.GetArgument<int>("id"))
            );

            Field<ListGraphType<ManufacturerType>>(
                "manufacturers",
                resolve: ctx => dbContext.Manufacturers.ToList()
            );

            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: ctx => dbContext.Orders.ToList()
            );
        }
    }
}
