using FManager.Data.Entities.Users;
using System.Threading.Tasks;

/// <summary>
/// Users service.
/// </summary>
namespace FManager.Core.Services
{
    public interface IUsersService : IService<User>
    {
        /// <summary>
        /// Get current user logged
        /// </summary>
        /// <returns></returns>
        Task<User> GetUserByCurrentSessionToken();
    }
}