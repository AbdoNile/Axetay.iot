using Axetay.Provision.Certificates;

namespace Axetay.Iot.Tests
{
    public class HardCodedConfigProvider : IS3CertificateStorageConfigs
    {
        public string BucketName => "axetay-thing-certificates";
    }
}