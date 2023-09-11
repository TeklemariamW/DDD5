using Azure.Identity;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureCosmosDB(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            IHostEnvironment environment2 = environment;
            string dataBaseId = configuration["ConnectionStrings:CosmosDb:AccountKey"];
            string accountEndPoint = configuration["ConnectionStrings:CosmosDb:DbName"];
            services.AddDbContext<RepositoryContext>(delegate (DbContextOptionsBuilder options)
            {
                if (accountEndPoint.Contains("AccountKey", StringComparison.OrdinalIgnoreCase))
                {
                    options.UseCosmos(accountEndPoint, dataBaseId);
                }
                else
                {
                    options.UseCosmos(accountEndPoint, new DefaultAzureCredential(), dataBaseId);
                }

                if(environment2.IsDevelopment())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }

                options.ConfigureWarnings(delegate (WarningsConfigurationBuilder w)
                {
                    w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
                });
            });
        }

        public static void EnsureReposityDataBaseExist(this IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<RepositoryContext>().Database.EnsureCreated();
        }
    }
}
