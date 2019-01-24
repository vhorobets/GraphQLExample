using GraphQL.Types;
using Persistence.Entities;

namespace WebApi.GraphQL.Types.InputTypes
{
    public class CustomerInputType : InputObjectGraphType<Customer>
    {
        public CustomerInputType()
        {
            Name = "customerInput";
            Field<NonNullGraphType<StringGraphType>>(nameof(Customer.FirstName));
            Field<NonNullGraphType<StringGraphType>>(nameof(Customer.LastName));
            Field<IntGraphType>(nameof(Customer.Age));
            Field<CustomerTypeEnumType>(nameof(Customer.CustomerType));
        }
    }
}
