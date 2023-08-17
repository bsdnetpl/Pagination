using Pagination.Models;

namespace Pagination.Services
{
    public interface IUserServices
    {
        List<User> CreateAllUsers(int amountOfUsers);
    }
}