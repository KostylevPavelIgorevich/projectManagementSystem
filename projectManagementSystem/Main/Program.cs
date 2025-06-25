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
            await UtilsPasswordMigrationHelper.HashAndUpdatePasswordForUser(userRepo, "pavel1", "qwe", cancellationToken); //����������� ��� ������ �������� � ������� ������ ������� ������������
            while (true)
            {
                Console.WriteLine("����� ����������!");
                Console.Write("������� �����: ");
                var login = Console.ReadLine();

                Console.Write("������� ������: ");
                var password = Console.ReadLine();

                var user = await userRepo.GetUserByLogin(login, cancellationToken);

                if (user != null && UtilsHeshPassword.VerifyPassword(password, user.Password))
                {
                    int currentUserId = user.Id;
                    Console.WriteLine($"�������� ���� ��� {user.Role}.");

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
                    Console.WriteLine("�������� ����� ��� ������.");
                    Console.Write("��������� �������? (��/���): ");
                    var choice = Console.ReadLine()?.ToLower();

                    if (choice != "��" && choice != "���")
                    {
                        Console.WriteLine("����� �� ���������.");
                        break;
                    }
                }
            }
        }

        private static async Task RunManagerInterface(IUserRepositories userRepo, IProjectTaskRepositories taskRepo, ILogRepositories logRepo, int currentUserId, CancellationToken cancellationToken)
        {
            while (true)
            {
                Console.WriteLine("\n�������� ��������:\n" +
                                  "1. �������� ������������.\n" + //+
                                  "2. ������� ������.\n" + //+
                                  "3. �������� ������������.\n" + //+
                                  "4. �������� ����� � ������ ������������.\n" + //+
                                  "5. �������� ������.\n" + //+ 
                                  "6. �������� ������ ������.\n" + 
                                  "7. ����� ������������ �� ������.\n" + //+
                                  "8. ����� ������������ �� �����.\n" + //+
                                  "9. ����� ������ �� ID.\n" + //+
                                  "10. �������� ������ �� ID ������������ ������������.\n" + //+
                                  "11. �������� ������ �� �������.\n" + //+
                                  "12. �������� ���� �� ID ������.\n" + //+
                                  "13. ������� ������������.\n" + //+
                                  "14. ������� ������.\n" + //+
                                  "15. ������� ���.\n" + //+
                                  "0. �����.\n");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("������� ���������� ����� ��������.");
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
                            Console.WriteLine("�����");
                            return;
                        default:
                            Console.WriteLine("�������� �����. ���������� �����.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"������: {ex.Message}");
                }
            }
        }

        private static async Task RunOrdinaryEmployeeInterface(IUserRepositories userRepo, IProjectTaskRepositories taskRepo, ILogRepositories logRepo, CancellationToken cancellationToken)
        {
            while (true)
            {
                Console.WriteLine("\n�������� ��������:\n" +
                                  "1. �������� ����� � ������ ������������.\n" +
                                  "2. �������� ������ ������.\n" +
                                  "3. �������� ������ �� ID.\n" +
                                  "4. �������� ������ �� ID ������������ ������������.\n" +
                                  "5. �������� ������ �� �������.\n" +
                                  "6. �������� ���� �� ID ������.\n" +
                                  "0. �����.\n");

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("������� ���������� ����� ��������.");
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
                            Console.WriteLine("�����");
                            return;
                        default:
                            Console.WriteLine("�������� �����. ���������� �����.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"������: {ex.Message}");
                }

            }
        }

    }
}
