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
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(StoreDbContext dbContext, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(p => p.Name);
            Field(p => p.Price);

            // simple solution
            //Field<ManufacturerType>("Manufacturer", resolve: ctx => ctx.Source.Manufacturer);

            // solution with data loader
            Func<IEnumerable<int>, Task<IDictionary<int, Manufacturer>>> getManufacturersFunc = async (ids) =>
            {          
                return await dbContext.Manufacturers.Where(m => ids.Contains(m.ManufacturerId)).ToDictionaryAsync(m => m.ManufacturerId, m => m);
            };

            Field<ManufacturerType>(
                "manufacturer",
                resolve: ctx =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddBatchLoader<int, Manufacturer>(
                        "GetManufacturerByManufacturerId", getManufacturersFunc
                    );
                    return loader.LoadAsync(ctx.Source.ManufacturerId);
                }
            );
        }
    }
}
