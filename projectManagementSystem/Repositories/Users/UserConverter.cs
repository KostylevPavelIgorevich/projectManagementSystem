using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;

namespace projectManagementSystem.Repositories.Users
{
    public static class UserConverter
    {
        public static User ToDomain(this UserDb userDb)
        {
            return new User(userDb.UserId, userDb.Role, userDb.Name, userDb.Password, userDb.Login);
        }
    }
}
