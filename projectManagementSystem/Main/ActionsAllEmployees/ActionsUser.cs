using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;
using projectManagementSystem.Repositories.ProjectsTasks;
using projectManagementSystem.Repositories.Users;
using projectManagementSystem.Utils;
using System.Threading.Tasks;

namespace projectManagementSystem.Main.ActionsAllEmployees
{
    public class ActionsUser
    {
        public static async Task AddUser(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            var name = UtilsReadIntParameter.ReadNonEmptyString("Введите имя пользователя:");
            var login = UtilsReadIntParameter.ReadNonEmptyString("Введите логин пользователя:");
            var rawPassword = UtilsReadIntParameter.ReadNonEmptyString("Введите пароль пользователя:");
            var hashedPassword = UtilsHeshPassword.HashPassword(rawPassword);

            UtilsReadIntParameter.UserRole? role = null;
            while (role == null)
            {
                role = UtilsReadIntParameter.ReadUserRole("Введите роль пользователя:");
            }

            var user = new User(0, name, hashedPassword, role.ToString(), login);
            int newUserId = await userRepo.AddUser(user, cancellationToken);
            Console.WriteLine($"Пользователь успешно создан. ID: {newUserId}");
        }
        public static async Task GetUserByLogin(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            var login = UtilsReadIntParameter.ReadNonEmptyString("Введите логин пользователя:");
            var user = await userRepo.GetUserByLogin(login, cancellationToken);
            Console.WriteLine($"ID: {user.Id} Имя: {user.Name} Логин: {user.Login} Роль: {user.Role}");
        }

        public static async Task GetUserByName(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            var name = UtilsReadIntParameter.ReadNonEmptyString("Введите имя пользователя:");
            var user = await userRepo.GetUserByName(name, cancellationToken);
            Console.WriteLine($"ID: {user.Id} Имя: {user.Name} Логин: {user.Login} Роль: {user.Role}");
        }

        public static async Task UpdateUser(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            int id = UtilsReadIntParameter.ReadIntParameter("Введите ID пользователя:");
            string name = UtilsReadIntParameter.ReadNonEmptyString("Введите новое имя:");

            UtilsReadIntParameter.UserRole? role = null;
            while (role == null)
            {
                role = UtilsReadIntParameter.ReadUserRole("Введите новую роль:");
            }

            await userRepo.UpdateUser(id, name, role.ToString(), cancellationToken);

            Console.WriteLine("Пользователь успешно обновлён.");
        }

        public static async Task UpdateLoginPasswordById(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            var id = UtilsReadIntParameter.ReadIntParameter("Введите ID пользователя:");
            var login = UtilsReadIntParameter.ReadNonEmptyString("Введите новый логин:");
            var rawPassword = UtilsReadIntParameter.ReadNonEmptyString("Введите новый пароль:");
            var hashedPassword = UtilsHeshPassword.HashPassword(rawPassword);
            await userRepo.UpdateLoginPasswordById(id, login, hashedPassword, cancellationToken);
            Console.WriteLine("Пользователь успешно обновлён.");
        }

        public static async Task DeleteUser(IUserRepositories userRepo, CancellationToken cancellationToken)
        {
            var name = UtilsReadIntParameter.ReadNonEmptyString("Введите имя пользователя:");
            await userRepo.DeleteUser(name, cancellationToken);
            Console.WriteLine($"Пользователь удален.");
        }
    }

}


