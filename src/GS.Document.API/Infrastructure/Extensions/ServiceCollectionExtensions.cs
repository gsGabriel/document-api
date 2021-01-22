using Amazon.S3;
using Confluent.Kafka;
using GS.Document.API.Infrastructure.Configs;
using GS.Document.API.Infrastructure.Swagger;
using GS.Document.Application.CommandHandlers;
using GS.Document.Application.Commands;
using GS.Document.Application.Framework.CommandHandler;
using GS.Document.Domain.Repositories;
using GS.Document.Domain.ValueObjects;
using GS.Document.Infra.Database;
using GS.Document.Infra.Database.Repositories;
using GS.Document.Infra.Kafka;
using GS.Document.Infra.Kafka.Interfaces;
using GS.Document.Infra.S3;
using GS.Document.Infra.S3.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GS.Document.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = configuration
                .GetSection(nameof(ProducerConfig))
                .Get<ProducerConfig>();

            var awsConfig = configuration
                .GetSection(nameof(AwsConfig))
                .Get<AwsConfig>();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<DocumentContext>(name: "database")
                //.AddS3(o =>
                //{
                //    o.S3Config.ServiceURL = awsConfig.ServiceS3Url;
                //}, name: "s3")
                .AddKafka(producerConfig, name: "kafka");

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<DocumentContext>(o => o.UseNpgsql(configuration.GetConnectionString("DocumentDatabase"), npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                }));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommandHandler<CreateDocumentCommand, DocumentId>, CreateDocumentCommandHandler>();
            services.AddAWSService<IAmazonS3>();
            services.AddTransient<IBucketService>(o =>
            {
                var s3Client = o.GetRequiredService<IAmazonS3>();
                var logger = o.GetRequiredService<ILogger<BucketService>>();
                return new BucketService(s3Client, logger);
            });
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEventProducer>(o => 
            {
                var producerConfig = configuration.GetSection(nameof(ProducerConfig)).Get<ProducerConfig>();
                var logger = o.GetRequiredService<ILogger<EventProducer>>();
                return new EventProducer(producerConfig, logger);
            });

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = true;
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

            services.AddVersionedApiExplorer(
               options =>
               {
                   options.GroupNameFormat = "'v'VVV";
                   options.SubstituteApiVersionInUrl = true;
                   options.AssumeDefaultVersionWhenUnspecified = true;
               });

            services.AddLogging();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.EnableAnnotations();
                    options.OperationFilter<SwaggerDefaultValues>();
                });

            return services;
        }
    }
}
