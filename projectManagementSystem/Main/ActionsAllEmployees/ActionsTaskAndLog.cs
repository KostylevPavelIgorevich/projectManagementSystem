using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;
using projectManagementSystem.Repositories.ProjectsTasks;
using projectManagementSystem.Utils;
using System.Threading.Tasks;

namespace projectManagementSystem.Main.ActionsAllEmployees
{
    public static class ActionsTaskAndLog
    {
        public static async Task AddProjectTask(IProjectTaskRepositories taskRepo, ILogRepositories logRepo, int currentUserId,CancellationToken cancellationToken)
        {
            var id = UtilsGenerateRandomNumber.GenerateNumber();
            var title = UtilsReadIntParameter.ReadNonEmptyString("Введите заголовок задачи:");
            var description = UtilsReadIntParameter.ReadNonEmptyString("Введите описание задачи:");
            var status = UtilsReadIntParameter.ReadTaskStatus("Введите статус задачи (ToDo / InProgress / Done):").ToString();
            var assignedUserId = UtilsReadIntParameter.ReadIntParameter("Введите ID назначенного пользователя:");
            var task = new ProjectTask(id, title, description, status, assignedUserId);
            await taskRepo.AddProjectTask(task, cancellationToken);
            var log = new Log
            (
                id: UtilsGenerateRandomNumber.GenerateNumber(),
                taskId: id,
                userId: currentUserId,
                message: $"Задача создана: {title}",
                createdAt: DateTime.Now
            );
            await logRepo.AddLog(log, cancellationToken);

            Console.WriteLine("Задача и лог успешно добавлены.");
        }
        public static async Task GetProjectTaskByTaskId(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var taskId = UtilsReadIntParameter.ReadIntParameter("Введите ID задачи:");
            var task = await taskRepo.GetProjectTaskByTaskId(taskId, cancellationToken);
            Console.WriteLine($"ID: {task.Id} Тема: {task.Title} Описание: {task.Description} Статус: {task.Status}");
        }

        public static async Task GetProjectTaskByAssignedUserId(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var userId = UtilsReadIntParameter.ReadIntParameter("Введите ID пользователя:");
            var tasks = await taskRepo.GetProjectTaskByAssignedUserId(userId, cancellationToken);

            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("Задачи для этого пользователя не найдены.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id} Тема: {task.Title} Описание: {task.Description} Статус: {task.Status}");
            }
        }

        public static async Task GetProjectTaskByStatus(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var status = UtilsReadIntParameter.ReadNonEmptyString("Введите статус задачи:");
            var tasks = await taskRepo.GetProjectTaskByStatus(status, cancellationToken);

            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("Задачи по данному статусу не найдены.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id} Тема: {task.Title} Описание: {task.Description} Статус: {task.Status} ID Сотрудника: {task.Assigneduserid}");
            }
        }

        public static async Task GetLogsByTaskId(ILogRepositories logRepo, CancellationToken cancellationToken)
        {
            var taskId = UtilsReadIntParameter.ReadIntParameter("Введите ID задачи:");
            var logs = await logRepo.GetLogsByTaskId(taskId, cancellationToken);

            foreach (var log in logs)
            {
                Console.WriteLine($"ID: {log.Id}, CreatedAt: {log.CreatedAt}");
            }
        }

        public static async Task UpdateProjectTask(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var id = UtilsReadIntParameter.ReadIntParameter("Введите ID задачи:");
            var title = UtilsReadIntParameter.ReadNonEmptyString("Введите заголовок:");
            var description = UtilsReadIntParameter.ReadNonEmptyString("Введите описание:");
            var statusEnum = UtilsReadIntParameter.ReadTaskStatus("Введите статус задачи:");
            var status = statusEnum.ToString(); 
            var assignedUserId = UtilsReadIntParameter.ReadIntParameter("Введите ID назначенного пользователя:");
            var task = new ProjectTask(id, title, description, status, assignedUserId);
            await taskRepo.UpdateProjectTask(task, cancellationToken);
        }
        public static async Task UpdateProjectStatusTaskByTaskId(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var id = UtilsReadIntParameter.ReadIntParameter("Введите ID задачи:");
            var newStatusEnum = UtilsReadIntParameter.ReadTaskStatus("Введите новый статус задачи:");
            var newStatus = newStatusEnum.ToString(); 
            await taskRepo.UpdateProjectStatusTaskByTaskId(id, newStatus, cancellationToken);
        }

        public static async Task DeleteProjectTaskByTaskId(IProjectTaskRepositories taskRepo, CancellationToken cancellationToken)
        {
            var id = UtilsReadIntParameter.ReadIntParameter("Введите ID задачи:");
            await taskRepo.DeleteProjectTaskByTaskId(id, cancellationToken);
            Console.WriteLine("Удаление прошло успешно.");
        }

        public static async Task DeleteLogById(ILogRepositories logRepo, CancellationToken cancellationToken)
        {
            var id = UtilsReadIntParameter.ReadIntParameter("Введите ID лога:");
            await logRepo.DeleteLogById(id, cancellationToken);
            Console.WriteLine("Удаление прошло успешно.");
        }
    }

}

