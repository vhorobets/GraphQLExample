using GraphQL.Types;
using Persistence.Entities;

namespace WebApi.GraphQL.Types
{
    public class CustomerTypeEnumType : EnumerationGraphType<CustomerTypeEnum>
    {
        public CustomerTypeEnumType()
        {
            Name = "CustomerTypeEnum";
            Description = "The type of customer";
        }
    }
}
