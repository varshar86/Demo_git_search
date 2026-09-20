using Searchify.Domain.Model;

namespace Searchify.Domain.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> AuthenticateUser(User user);
    }
}
