using Gauss.Investment.Domain.Entities;

namespace Gauss.Investment.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
