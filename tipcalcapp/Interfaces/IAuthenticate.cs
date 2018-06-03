using System.Threading.Tasks;

namespace tipcalc.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();
    }
}
