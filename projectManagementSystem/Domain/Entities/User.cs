namespace projectManagementSystem.Domain.Entities
{
    public class User
    {
        public User(int id, string name, string password, string role, string login) 
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
            Login = login;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Login {  get; set; }
    }
}
