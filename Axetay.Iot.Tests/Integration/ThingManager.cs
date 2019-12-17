using Amazon.IoT;
using Amazon.S3;
using Axetay.Provision;
using Axetay.Provision.Certificates;
using NUnit.Framework;

namespace Axetay.Iot.Tests.Integration
{
    public class ThingManager
    {
        private AwsIotThingManager _amazonManager;

        [SetUp]
        public void Setup()
        {
            IAmazonIoT iotClient = new AmazonIoTClient();
            IAmazonS3 s3Client = new AmazonS3Client();
            IS3CertificateStorageConfigs s3CertificateStorageConfigs = new HardCodedConfigProvider();
            ICertificateStore certificateStore = new S3CertificateStorage(s3Client, s3CertificateStorageConfigs);
            _amazonManager = new AwsIotThingManager(iotClient, certificateStore);
        }

        [Test]
        public void Test1()
        {
            _ = _amazonManager.RegisterThing("MyButton", "Zorrar");

        }
    }
}