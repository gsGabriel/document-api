using GS.Document.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace GS.Document.API.Infrastructure.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateWalletContext(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<DocumentContext>>();

                try
                {
                    var context = services.GetService<DocumentContext>();

                    logger.LogInformation("Trying migration");

                    var retries = 3;
                    var retry = Policy.Handle<Exception>()
                        .WaitAndRetry(
                            retryCount: retries,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retry}. Re-trying", nameof(DocumentContext), exception.GetType().Name, exception.Message, retry, retries);
                            });

                    retry.Execute(() =>
                    {
                        context.Database.Migrate();
                    });

                    logger.LogInformation("Migration completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on {DbContextName}", typeof(DocumentContext).Name);
                }
            }

            return webHost;
        }
    }
}
