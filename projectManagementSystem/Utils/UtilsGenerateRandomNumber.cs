namespace projectManagementSystem.Utils
{
    public static class UtilsGenerateRandomNumber
    {
        public static int GenerateNumber()
        {
            return new Random().Next(100000, 999999);
        }
    }
}
