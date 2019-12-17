using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Axetay.Provision.Certificates
{
    public class S3CertificateStorage : ICertificateStore
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IS3CertificateStorageConfigs _configs;
        public S3CertificateStorage(IAmazonS3 s3Client, IS3CertificateStorageConfigs configs)
        {
            _s3Client = s3Client;
            _configs = configs;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public async Task StoreCertificate(string certificateId, string pemFile)
        {
            await using var stream = GenerateStreamFromString(pemFile);
            var request = new PutObjectRequest()
            {
                BucketName = _configs.BucketName,
                InputStream = stream,
                Key = certificateId
            };

            var response = await _s3Client.PutObjectAsync(request);
        }
    }
}