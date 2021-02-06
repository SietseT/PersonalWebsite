using GraphQL.Server;
using GraphQL.Types;
using Har.Application.Components;
using Har.Application.GraphQL.Interfaces;
using Har.Application.GraphQL.Queries;
using Har.Application.GraphQL.Schemas;
using Har.Application.GraphQL.Types;
using Har.Application.Services;
using Har.Infrastructure;
using Har.Infrastructure.Data.Kontent.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Har
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            
            services.AddSingleton<IComponentParser<string>, HtmlComponentParser>();
            
            services.AddSingleton<IProjectRepository, KontentProjectRepository>();

            services.AddSingleton<ComponentsType>();
            
            services.AddSingleton<ProjectType>();
            services.AddSingleton<ProjectsQuery>();
            
            services.AddSingleton<ComponentInterface>();
            services.AddSingleton<TextComponentType>();

            services.AddSingleton<ISchema, ProjectsSchema>();

            services.AddGraphQL()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddSystemTextJson();
            
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<ISchema>();
            
            app.UseGraphiQLServer();
            app.UseHttpsRedirection();
            
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}