using FluentMigrator.Runner;
using IntelligentHabitacion.Api.Application;
using IntelligentHabitacion.Api.Configuration.Swagger;
using IntelligentHabitacion.Api.Configuration.Token;
using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.Infrastructure;
using IntelligentHabitacion.Api.Infrastructure.Migrations;
using IntelligentHabitacion.Api.Middleware;
using IntelligentHabitacion.Api.Services;
using IntelligentHabitacion.Api.WebSocket.AddFriend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

#pragma warning disable ASP0000
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
        public IConfiguration Configuration { get; }

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
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddHashids(setup =>
            {
                setup.Salt = Configuration.GetValue<string>("Settings:IdCryptographySalt");
                setup.MinHashLength = 3;
            });

            services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Application.Services.AutoMapper.AutoMapping(provider.GetService<HashidsNet.IHashids>()));
            }).CreateMapper());

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddVersionedApiExplorer(options =>
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
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"{this.GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? $"{GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
                    });
                }

                options.SchemaFilter<SwaggerSubtypeOfAttributeFilter>();
                options.SchemaFilter<EnumSchemaFilter>();
                options.IncludeXmlComments("IntelligentHabitacion.Api.xml");
                options.OperationFilter<HashidsOperationFilter>();
            });

            services
                .AddUseCases(Configuration)
                .AddRepositories(Configuration)
                .AddTokenController(Configuration);

            services.AddHttpContextAccessor();
            services.AddHostedService<RunAtMidnightEveryDay>();

            services.AddScoped<AuthenticationUserAttribute>();
            services.AddScoped<AuthenticationUserIsPartOfHomeAttribute>();
            services.AddScoped<AuthenticationUserIsAdminAttribute>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddMySql5()
                    .WithGlobalConnectionString($"{Configuration.GetConnectionString("Connection")}Database={Configuration.GetConnectionString("DatabaseName")};")
                    .ScanIn(Assembly.Load("IntelligentHabitacion.Api.Infrastructure")).For.All());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseMiddleware<CultureMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", "IntelligentHabitacion.Api - " + description.GroupName.ToUpperInvariant());
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AddFriendHub>("/addNewFriend");
            });

            Database.EnsureDatabase(Configuration.GetConnectionString("Connection"), Configuration.GetConnectionString("DatabaseName"));

            app.Migrate();
        }
    }
}
