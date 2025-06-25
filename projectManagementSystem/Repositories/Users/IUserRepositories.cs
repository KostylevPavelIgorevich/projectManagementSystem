using projectManagementSystem.Domain.Entities;

namespace projectManagementSystem.Repositories.Users
{
    public interface IUserRepositories
    {
        Task<int> AddUser(User user, CancellationToken cancellationToken);
        Task<User> GetUserByLogin(string login, CancellationToken cancellationToken);
        Task DeleteUser(string name, CancellationToken cancellationToken);
        Task UpdateUser(int id, string name, string role, CancellationToken cancellationToken);
        Task<User> GetUserByName(string name, CancellationToken cancellationToken);
        Task UpdateLoginPasswordById(int id, string login, string password, CancellationToken cancellationToken);
    }
}
