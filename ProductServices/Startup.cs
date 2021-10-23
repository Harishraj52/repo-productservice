using Microsoft.Extensions.Configuration;
//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------

namespace Product_Services
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using ProductService.Core.Services;
    using ProductService.Data.Context;
    using FluentValidation.AspNetCore;
    using System;
    using ProductService.Data.Models;
    using ProductService.Data.Entities;
    using ProductService.Data.FluentValidators;

    /// <summary>
    ///  This class has Startup functionalities.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        ///  Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        ///  add services to the container.
        /// </summary>
        /// <param name="services">To add the different services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                   // fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.DisableDataAnnotationsValidation = true;
                    fv.RegisterValidatorsFromAssemblyContaining<DtoValidators>();
                });
                

            // .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve)
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<ProductServiceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductDb")));
            // services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();

            // services.AddScoped< ProductServiceContext _context>();
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductService", Version = "v1" });
            });
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">To configure the HTTP request pipeline.</param>
        /// <param name="env">To specify the environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ProductServiceContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
