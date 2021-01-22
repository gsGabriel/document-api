using Amazon.Runtime;
using Amazon.S3;
using GS.Document.Infra.S3.Interfaces;
using Microsoft.Extensions.Logging;
using System.IO;
using Xunit;

namespace GS.Document.Infra.S3.Tests
{
    public class BucketServiceIntegrationTest
    {
        private readonly IAmazonS3 s3Client;
        private readonly IBucketService bucketService;

        public BucketServiceIntegrationTest()
        {
            s3Client = new AmazonS3Client(
                new AmazonS3Config
                {
                    ServiceURL = "http://localhost:5002",
                    UseHttp = true,
                    ProxyHost = "localhost",
                    ProxyPort = 5002
                });

            bucketService = new BucketService(s3Client, LoggerFactory.Create(x => x.AddConsole()).CreateLogger<BucketService>());
        }

        [Fact(Skip = "Dependency needed")]
        public async void Should_upload_fileAsync()
        {
            //given
            var file = new MemoryStream();

            //when
            var path = await bucketService.UploadAsync(file);

            //then
            Assert.False(string.IsNullOrEmpty(path));
        }

        [Fact(Skip = "Dependency needed")]
        public async void Should_download_file()
        {
            //given
            var file = new MemoryStream();
            var path = await bucketService.UploadAsync(file);

            //when
            var stream = await bucketService.DownloadAsync(path);

            //then
            Assert.True(stream.CanRead);
        }
    }
}
