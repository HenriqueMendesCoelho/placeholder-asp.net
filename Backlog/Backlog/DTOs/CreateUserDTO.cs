namespace Backlog.DTOs
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly BithDate { get; set; }
        public int Age { get; set; }
        public string CpfOrCnpj { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BackupEmail { get; set; } = string.Empty;
        public CreateUserDTO()
        {

        }
        public CreateUserDTO(string name, DateOnly bithDate, int age, string cpfOrCnpj, string password, string email, string backupEmail)
        {
            Name = name;
            BithDate = bithDate;
            Age = age;
            CpfOrCnpj = cpfOrCnpj;
            Password = password;
            Email = email;
            BackupEmail = backupEmail;
        }
    }
}
