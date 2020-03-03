﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Reflection;

namespace IntelligentHabitacion.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore().AddJsonFormatters().AddVersionedApiExplorer(
              options =>
              {
                  options.GroupNameFormat = "'v'VVV";
                  options.SubstituteApiVersionInUrl = true;
              });

            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services.AddSwaggerGen(options => {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                for (var indice = provider.ApiVersionDescriptions.Count - 1; indice >= 0; indice--)
                {
                    var description = provider.ApiVersionDescriptions.ElementAt(indice);
                    options.SwaggerDoc(description.GroupName, new Info()
                    {
                        Title = $"{this.GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? $"{GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
                    });
                }

                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments("IntelligentHabitacion.Api.xml");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", "IntelligentHabitacion.Api - " + description.GroupName.ToUpperInvariant());
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
