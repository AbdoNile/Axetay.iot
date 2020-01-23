using System.Threading.Tasks;

namespace Axetay.Provision.Certificates
{
    /// <summary>
    /// Handles certificate storage and retrieval
    /// </summary>
    public interface ICertificateStore
    {
        /// <summary>
        /// Store a certificate
        /// </summary>
        /// <param name="certificateId"></param>
        /// <param name="pemFile"></param>
        /// <returns></returns>
        Task StoreCertificate(string certificateId, string pemFile);
    }
}