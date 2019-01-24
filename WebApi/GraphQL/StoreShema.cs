using GraphQL;
using GraphQL.Types;

namespace WebApi.GraphQL
{
    public class StoreSchema : Schema // GrapgQL Schema
    {
        public StoreSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<StoreQuery>();
            Mutation = resolver.Resolve<StoreMutation>();
        }
    }
}
