using GraphQL.Types;
using Persistence;
using Persistence.Entities;
using WebApi.GraphQL.Types;
using WebApi.GraphQL.Types.InputTypes;

namespace WebApi.GraphQL
{
    public class StoreMutation : ObjectGraphType
    {
        public StoreMutation(StoreDbContext dbContext)
        {
            Field<CustomerType>(
                "createCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }
                ),
                resolve: ctx => 
                {
                    var customer = ctx.GetArgument<Customer>("customer");
                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                    return customer;
                }
            );
        }
    }
}
