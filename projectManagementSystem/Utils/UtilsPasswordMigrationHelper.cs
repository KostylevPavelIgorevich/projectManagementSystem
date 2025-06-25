using projectManagementSystem.Repositories.Users;

namespace projectManagementSystem.Utils
{
    public class UtilsPasswordMigrationHelper
    {
        public static async Task HashAndUpdatePasswordForUser(IUserRepositories userRepo, string userLogin, string rawPassword, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetUserByLogin(userLogin, cancellationToken);
            if (user == null)
            {
                Console.WriteLine($"Пользователь с логином '{userLogin}' не найден.");
                return;
            }
            string hashedPassword = UtilsHeshPassword.HashPassword(rawPassword);
            await userRepo.UpdateLoginPasswordById(user.Id, user.Login, hashedPassword, cancellationToken);
            Console.WriteLine($"Пароль пользователя '{userLogin}' успешно захеширован и обновлен в базе.");
        }
    }
}

