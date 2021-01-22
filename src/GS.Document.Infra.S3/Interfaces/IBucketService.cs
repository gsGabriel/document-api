using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Infra.S3.Interfaces
{
    public interface IBucketService
    {
        Task<string> UploadAsync(Stream file, CancellationToken cancellationToken = default);
        Task<Stream> DownloadAsync(string path, CancellationToken cancellationToken = default);
    }
}
