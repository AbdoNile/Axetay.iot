using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.IoT;
using Amazon.IoT.Model;
using Axetay.Provision.Certificates;
using Stream = System.IO.Stream;

namespace Axetay.Provision
{
    public class AwsIotThingManager : IThingManager
    {
        private readonly Amazon.IoT.IAmazonIoT _client;
        private readonly ICertificateStore _certificateStore;

        public AwsIotThingManager(IAmazonIoT client, ICertificateStore certificateStore)
        {
            _client = client;
            _certificateStore = certificateStore;
          
        }

        public async Task RegisterThing(string thingName, string thingType)
        {
            var createThingGroupRequest = new CreateThingRequest()
            {
                ThingName = thingName,
                ThingTypeName = thingType
            };
             
            var createThingResponse = await _client.CreateThingAsync(createThingGroupRequest);
            var certificate = await CreateCertificate();
            await _certificateStore.StoreCertificate(certificate.CertificateId, certificate.CertificatePem);
            await AttachThingToCertificate(createThingResponse.ThingName, certificate.CertificateArn);
            await CreatePolicy(thingType);
            await AttachPolicyToCertificate(thingType, certificate.CertificateArn);
        }

        private async Task<CreateKeysAndCertificateResponse> CreateCertificate()
        {
            var result = await _client.CreateKeysAndCertificateAsync();

            return result;
        }

        private async Task AttachThingToCertificate(string thingName, string certificateArn)
        {
            var request = new AttachThingPrincipalRequest()
            {
                ThingName = thingName,
                Principal = certificateArn
            };
            
            var result = await _client.AttachThingPrincipalAsync(request);
        }

        private async Task CreatePolicy(string policyName)
        {
            var existingPolicy = await _client.GetPolicyAsync(policyName);
            if (existingPolicy == null)
            {
                var createPolicyRequest = new CreatePolicyRequest()
                {
                    PolicyDocument = LoadPolicy(policyName),
                    PolicyName = policyName
                };
                await _client.CreatePolicyAsync(createPolicyRequest);
            }
            else
            {
                CreatePolicyVersionRequest createPolicyVersionRequest = new CreatePolicyVersionRequest()
                {
                    PolicyDocument = LoadPolicy(policyName),
                    PolicyName = policyName,
                    SetAsDefault = true
                };
                await _client.CreatePolicyVersionAsync(createPolicyVersionRequest);
            }
        }

        private async Task AttachPolicyToCertificate(string policyName, string certificateArn)
        {
            var request = new AttachPolicyRequest()
            {
                PolicyName = policyName,
                Target = certificateArn
            };

            var result = await _client.AttachPolicyAsync(request);
        }

        private string LoadPolicy(string policyName)
        {
            var policyLocator = $"Axetay.Provision.Policies.{policyName}.json";
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(policyLocator);
            using StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException());
            string result = reader.ReadToEnd();
            return result;
        }


    }
}
