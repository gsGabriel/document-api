using Amazon.S3;
using Amazon.S3.Model;
using GS.Document.Infra.S3.Interfaces;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Infra.S3
{
    public class BucketService : IBucketService
    {
        private readonly IAmazonS3 s3Client;
        private readonly ILogger<BucketService> logger;
        const string BUCKET_NAME = "documents";

        public BucketService(IAmazonS3 s3Client, ILogger<BucketService> logger)
        {
            this.s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> UploadAsync(Stream file, CancellationToken cancellationToken = default)
        {
            var key = $"/cdn/d/{Guid.NewGuid()}.pdf";

            var policy = Policy.Handle<Exception>()
                   .WaitAndRetryAsync(
                            retryCount: 3,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retry}. Re-trying", exception.GetType().Name, exception.Message, retry);
                            });

            await policy.ExecuteAsync(async token =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await s3Client.EnsureBucketExistsAsync(BUCKET_NAME);

                    await s3Client.PutObjectAsync(new PutObjectRequest
                    {
                        InputStream = file,
                        ContentType = "application/pdf",
                        BucketName = BUCKET_NAME,
                        Key = key,
                        CannedACL = S3CannedACL.PublicRead
                    }, token);
                }
            }, cancellationToken);

            logger.LogInformation("Upload to S3 was completed.");
            return key;
        }

        public async Task<Stream> DownloadAsync(string path, CancellationToken cancellationToken = default)
        {
            var policy = Policy.Handle<Exception>()
                  .WaitAndRetryAsync(
                           retryCount: 3,
                           sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                           onRetry: (exception, timeSpan, retry, ctx) =>
                           {
                               logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retry}. Re-trying", exception.GetType().Name, exception.Message, retry);
                           });

            GetObjectResponse response = null;
            await policy.ExecuteAsync(async token =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await s3Client.EnsureBucketExistsAsync(BUCKET_NAME);

                    response = await s3Client.GetObjectAsync(new GetObjectRequest
                    {
                        Key = path
                    }, token);
                }
            }, cancellationToken);

            logger.LogInformation("Download from S3 was completed.");

            return response.ResponseStream;
        }
    }
}