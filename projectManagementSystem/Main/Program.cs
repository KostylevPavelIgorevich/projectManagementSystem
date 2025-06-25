using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Main.ActionsAllEmployees;
using projectManagementSystem.Repositories;
using projectManagementSystem.Repositories.LogsEntrys;
using projectManagementSystem.Repositories.ProjectsTasks;
using projectManagementSystem.Repositories.Users;
using projectManagementSystem.Utils;

namespace projectManagementSystem.Main
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            string connectionString = ConnectionString.BaseConnectionStringPg;
            IUserRepositories userRepo = new UserRepositories(connectionString);
            await UtilsPasswordMigrationHelper.HashAndUpdatePasswordForUser(userRepo, "pavel1", "qwe", cancellationToken); //использовал для обхода проблемы с паролем самого первого пользователя
            while (true)
            {
                Console.WriteLine("Добро пожаловать!");
                Console.Write("Введите логин: ");
                var login = Console.ReadLine();

                Console.Write("Введите пароль: ");
                var password = Console.ReadLine();

                var user = await userRepo.GetUserByLogin(login, cancellationToken);

                if (user != null && UtilsHeshPassword.VerifyPassword(password, user.Password))
                {
                    int currentUserId = user.Id;
                    Console.WriteLine($"Успешный вход как {user.Role}.");

                    if (user.Role.ToLower() == "menedger")
                    {
                        await RunManagerInterface(userRepo, new ProjectTaskRepositories(connectionString), new LogRepositories(connectionString), currentUserId, cancellationToken);
                    }
                    else
                    {
                        await RunOrdinaryEmployeeInterface(userRepo, new ProjectTaskRepositories(connectionString), new LogRepositories(connectionString), cancellationToken);
                    }

                    break; 
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль.");
                    Console.Write("Повторить попытку? (да/нет): ");
                    var choice = Console.ReadLine()?.ToLower();

                    if (choice != "да" && choice != "нет")
                    {
                        Console.WriteLine("Выход из программы.");
                        break;
                    }
                }
            }
        }

        private static async Task RunManagerInterface(IUserRepositories userRepo, IProjectTaskRepositories taskRepo, ILogRepositories logRepo, int currentUserId, CancellationToken cancellationToken)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:\n" +
                                  "1. Добавить пользователя.\n" + //+
                                  "2. Создать задачу.\n" + //+
                                  "3. Обновить пользователя.\n" + //+
                                  "4. Обновить логин и пароль пользователя.\n" + //+
                                  "5. Обновить задачу.\n" + //+ 
                                  "6. Обновить статус задачи.\n" + 
                                  "7. Вывод пользователя по логину.\n" + //+
                                  "8. Вывод пользователя по имени.\n" + //+
                                  "9. Вывод задачи по ID.\n" + //+
                                  "10. Получить задачи по ID назначенного пользователя.\n" + //+
                                  "11. Получить задачи по статусу.\n" + //+
                                  "12. Получить логи по ID задачи.\n" + //+
                                  "13. Удалить пользователя.\n" + //+
                                  "14. Удалить задачу.\n" + //+
                                  "15. Удалить лог.\n" + //+
                                  "0. Выход.\n");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("Введите корректный номер действия.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            await ActionsUser.AddUser(userRepo, cancellationToken); break;
                        case 2:
                            await ActionsTaskAndLog.AddProjectTask(taskRepo, logRepo, currentUserId, cancellationToken); break;
                        case 3:
                            await ActionsUser.UpdateUser(userRepo, cancellationToken); break;
                        case 4:
                            await ActionsUser.UpdateLoginPasswordById(userRepo, cancellationToken); break;
                        case 5:
                            await ActionsTaskAndLog.UpdateProjectTask(taskRepo, cancellationToken); break;
                        case 6:
                            await ActionsTaskAndLog.UpdateProjectStatusTaskByTaskId(taskRepo, cancellationToken); break;
                        case 7:
                            await ActionsUser.GetUserByLogin(userRepo, cancellationToken); break; 
                        case 8:
                            await ActionsUser.GetUserByName(userRepo, cancellationToken); break;
                        case 9:
                            await ActionsTaskAndLog.GetProjectTaskByTaskId(taskRepo, cancellationToken); break;
                        case 10:
                            await ActionsTaskAndLog.GetProjectTaskByAssignedUserId(taskRepo, cancellationToken); break;
                        case 11:
                            await ActionsTaskAndLog.GetProjectTaskByStatus(taskRepo, cancellationToken); break;
                        case 12:
                            await ActionsTaskAndLog.GetLogsByTaskId(logRepo, cancellationToken); break;
                        case 13:
                            await ActionsUser.DeleteUser(userRepo, cancellationToken); break;
                        case 14:
                            await ActionsTaskAndLog.DeleteProjectTaskByTaskId(taskRepo, cancellationToken); break;
                        case 15:
                            await ActionsTaskAndLog.DeleteLogById(logRepo, cancellationToken); break;
                        case 0:
                            Console.WriteLine("Выход");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static async Task RunOrdinaryEmployeeInterface(IUserRepositories userRepo, IProjectTaskRepositories taskRepo, ILogRepositories logRepo, CancellationToken cancellationToken)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:\n" +
                                  "1. Обновить логин и пароль пользователя.\n" +
                                  "2. Обновить статус задачи.\n" +
                                  "3. Получить задачу по ID.\n" +
                                  "4. Получить задачи по ID назначенного пользователя.\n" +
                                  "5. Получить задачи по статусу.\n" +
                                  "6. Получить логи по ID задачи.\n" +
                                  "0. Выход.\n");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("Введите корректный номер действия.");
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            await ActionsUser.UpdateLoginPasswordById(userRepo, cancellationToken); break;
                        case 2:
                            await ActionsTaskAndLog.UpdateProjectStatusTaskByTaskId(taskRepo, cancellationToken); break;
                        case 3:
                            await ActionsTaskAndLog.GetProjectTaskByTaskId(taskRepo, cancellationToken); break;
                        case 4:
                            await ActionsTaskAndLog.GetProjectTaskByAssignedUserId(taskRepo, cancellationToken); break;
                        case 5:
                            await ActionsTaskAndLog.GetProjectTaskByStatus(taskRepo, cancellationToken); break;
                        case 6:
                            await ActionsTaskAndLog.GetLogsByTaskId(logRepo, cancellationToken); break;
                        case 0:
                            Console.WriteLine("Выход");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

            }
        }

    }
}
