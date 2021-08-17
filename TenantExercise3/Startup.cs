using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Multitenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.ActionFilters;
using TenantExercise3.Models;
using TenantExercise3.Services;
using TenantExercise3.Tenancy;

namespace TenantExercise3
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

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TenantExercise3", Version = "v1" });
                c.OperationFilter<TenantHeaderOperationFilter>();
            });

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddAutofacMultitenantRequestServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<TenantResolver>().As<ITenantResolver>().SingleInstance();
            builder.RegisterType<TenantResolverStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            //builder.RegisterType<DataService>().As<IDataService>();
            //builder.RegisterInstance(new OperationIdService()).SingleInstance();
            builder.Register(container =>
            {
                ITenantIdentificationStrategy strategy = container.Resolve<ITenantIdentificationStrategy>();
                strategy.TryIdentifyTenant(out object id);
                if (id != null)
                {
                    if (container.IsRegistered(typeof(ITenantResolver)))
                    {
                        var tenantResolver = container.Resolve<ITenantResolver>();
                        return tenantResolver.ResolveAsync(id).Result;
                    }
                }
                return new Tenant();
            }).InstancePerLifetimeScope();
        }

        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            var strategy = container.Resolve<ITenantIdentificationStrategy>();
            var multitenantContainer = new MultitenantContainer(strategy, container);
            multitenantContainer.ConfigureTenant("80fdb3c0-5888-4295-bf40-ebee0e3cd8f3", containerBuilder =>
            {
                containerBuilder.RegisterType<DataService>().As<IDataService>();
                containerBuilder.RegisterInstance(new OperationIdService()).SingleInstance();
            });
            return multitenantContainer;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TenantExercise3 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
