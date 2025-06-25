namespace projectManagementSystem.Utils
{
    public static class UtilsReadIntParameter
    {
        public enum TaskStatus
        {
            ToDo,
            InProgress,
            Done
        }
        public enum UserRole
        {
            Manager,
            Employee
        }
        public static int ReadIntParameter(string textForUser)
        {
            Console.WriteLine(textForUser);
            int inputParameter = 0;
            while (!int.TryParse(Console.ReadLine(), out inputParameter))
            {
                Console.WriteLine($"Вы ввели некорректное значение, пожалуйста введите число");
            }
            return inputParameter;
        }
        public static string ReadNonEmptyString(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Значение не может быть пустым. Повторите ввод:");
                input = Console.ReadLine();
            }
            return input;
        }
        public static UserRole? ReadUserRole(string prompt)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Доступные роли: Manager, Employee");
            var input = Console.ReadLine()?.Trim();

            if (Enum.TryParse<UserRole>(input, ignoreCase: true, out var role))
            {
                return role;
            }

            Console.WriteLine("Ошибка: недопустимая роль.");
            return null;
        }
        public static TaskStatus ReadTaskStatus(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine("Доступные статусы: ToDo, InProgress, Done");
                var input = Console.ReadLine()?.Trim();
                if (Enum.TryParse<TaskStatus>(input, ignoreCase: true, out var status))
                {
                    return status;
                }
                Console.WriteLine("Ошибка: недопустимый статус. Попробуй ещё раз.");
            }
        }

    }
}
