using System.Threading.Tasks;

namespace Okta_Web.Interfaces
{
    public interface IOktaClient
    {
        Task<string> CreateUser();
    }
}
