using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence;
using WebApi.GraphQL;

namespace WebApi
{
    public class Startup
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Store;Trusted_Connection=True;";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // register db context
            services.AddDbContext<StoreDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString));

            // register dep resolver for GraphQL & schema
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<StoreSchema>();

            // register GraphQL types
            services.AddGraphQL(options => { options.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<StoreSchema>(); // using graphQL schema
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); // using graphQL ui for building queries
            app.UseMvc();
        }
    }
}
