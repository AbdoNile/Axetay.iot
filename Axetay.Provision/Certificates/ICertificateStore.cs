using System.Threading.Tasks;

namespace Axetay.Provision.Certificates
{
    public interface ICertificateStore
    {
        Task StoreCertificate(string certificateId, string pemFile);
    }
}