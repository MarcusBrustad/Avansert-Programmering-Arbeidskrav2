using TodoApi.Models;

namespace TodoApi.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    
}