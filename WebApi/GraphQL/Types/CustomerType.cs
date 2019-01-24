using GraphQL.Types;
using Persistence.Entities;

namespace WebApi.GraphQL.Types
{
    public class CustomerType : ObjectGraphType<Customer> // type for GraphQL
    {
        public CustomerType()
        {
            Field(c => c.CustomerId);
            Field(c => c.FirstName);
            Field(c => c.LastName);
            Field(c => c.Age, nullable: true); // TODO: check how to deal with nulls
            //Field(c => c.CustomerType); // TODO: check how to show enum int value
            Field<CustomerTypeEnumType>("CustomerType");
        }
    }
}
