using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.Middleware;
using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Services.WebSocket;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.EmailHelper;
using IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.SetOfRules.Token;
using IntelligentHabitacion.Api.SetOfRules.Token.JWT;
using Microsoft.AspNetCore.Builder;
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
        private AppSettingsManager appSettingsManager { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            appSettingsManager = new AppSettingsManager(environment);
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
            services.AddSignalR();

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

            RegisterSetOfRules(services);
            RegisterRepository(services);

            services.AddScoped<ICryptographyPassword, CryptographyPassword>(ServiceProvider =>
            {
                return new CryptographyPassword(appSettingsManager.KeyAdditionalCryptography());
            });

            services.AddHttpContextAccessor();

            services.AddScoped<AuthenticationUserAttribute>();
            services.AddScoped<AuthenticationUserIsAdminAttribute>();
            services.AddScoped<ILoggedUser, LoggedUser>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<ITokenController, TokenController>(ServiceProvider =>
            {
                return new TokenController(appSettingsManager.ExpirationTimeMinutes());
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<WebSocketAddFriendHub>("/addNewFriend");
            });

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
            app.UseMvc();
        }

        private void RegisterSetOfRules(IServiceCollection services)
        {
            var listClassIntelligentHabitacionRules = Assembly.Load("IntelligentHabitacion.Api.SetOfRules").GetExportedTypes().Where(type => !type.IsAbstract && !type.IsGenericType &&
                    type.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.StartsWith("I") && interfaces.Name.EndsWith("Rule"))).ToList();

            foreach (var classRule in listClassIntelligentHabitacionRules)
            {
                var interfaceToRegister = classRule.GetInterfaces().Single(i => i.Name.StartsWith("I") && i.Name.EndsWith("Rule"));
                services.AddScoped(interfaceToRegister, classRule);
            }
        }

        private void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<IDatabaseInformations, DatabaseInformations>(ServiceProvider =>
            {
                return new DatabaseInformations(appSettingsManager.ConnectionString(), DatabaseType.MySql);
            });

            var listClassIntelligentHabitacionRules = Assembly.Load("IntelligentHabitacion.Api.Repository").GetExportedTypes().Where(type => !type.IsAbstract && !type.IsGenericType &&
                    type.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.StartsWith("I") && interfaces.Name.EndsWith("Repository") && !interfaces.Name.Equals("IBaseRepository") && !interfaces.Name.Equals("IDatabaseTypeRepository"))).ToList();

            foreach (var classRule in listClassIntelligentHabitacionRules)
            {
                var interfaceToRegister = classRule.GetInterfaces().Single(i => i.Name.StartsWith("I") && i.Name.EndsWith("Repository"));
                services.AddTransient(interfaceToRegister, classRule);
            }
        }
    }
}
