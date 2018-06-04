using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace tipcalc.Interfaces
{
    public interface IAuthenticate
    {
        PublicClientApplication AuthClient { get; set; }

        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();

        bool Logout();
    }
}
