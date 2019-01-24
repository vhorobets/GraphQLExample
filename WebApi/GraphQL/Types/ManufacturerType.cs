using System;
using System.Collections;
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
    public class ManufacturerType : ObjectGraphType<Manufacturer>
    {
        public ManufacturerType(StoreDbContext dbContext, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(m => m.Name);
            Field(m => m.Country);

            // version without data loader (db call for each item)
            //Field<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: ctx => ctx.Source.Products // put this from source because we're using lazy loading lib, instead of this we can get same result from dbContext
            //);

            //imitation of repository with certain method to get products by manufacturer
            // version with ILookup
            Func<IEnumerable<int>, Task<ILookup<int, Product>>> getProductsFunc = async (ids) =>
            {
                return (await dbContext.Products.Where(p => ids.Contains(p.ManufacturerId)).ToListAsync()).ToLookup(p => p.ManufacturerId);
            };

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: ctx =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Product>(
                        "GetProductsByManufacturerId", getProductsFunc
                    );
                    return loader.LoadAsync(ctx.Source.ManufacturerId);
                }
            );

            // version with IEnumerable
            //Func<IEnumerable<int>, Task<IEnumerable<Product>>> getProductsFunc = async (ids) => {
            //    return await dbContext.Products.Where(p => ids.Contains(p.ManufacturerId)).ToListAsync();
            //};

            //Field<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: ctx =>
            //    {
            //        var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Product>(
            //            "GetProductsByManufacturerId", getProductsFunc, (p) => p.ManufacturerId
            //        );
            //        return loader.LoadAsync(ctx.Source.ManufacturerId);
            //    }
            //);
        }
    }
}
