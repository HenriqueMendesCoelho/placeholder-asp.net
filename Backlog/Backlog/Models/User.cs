namespace Backlog.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateOnly BithDate { get; set; }
        public int Age { get; set; }
        public string CpfOrCnpj { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BackupEmail { get; set; } = string.Empty;
        public User()
        {
        
        }
        public User(string name, DateTime createdDate, DateOnly bithDate, int age, string cpfOrCnpj, string password, string email, string backupEmail)
        {
            Name = name;
            CreatedDate = createdDate;
            BithDate = bithDate;
            Age = age;
            CpfOrCnpj = cpfOrCnpj;
            Password = password;
            Email = email;
            BackupEmail = backupEmail;
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                Id == user.Id &&
                Name == user.Name &&
                CreatedDate == user.CreatedDate &&
                BithDate == user.BithDate &&
                Age == user.Age &&
                CpfOrCnpj == user.CpfOrCnpj &&
                Password == user.Password &&
                Email == user.Email &&
                BackupEmail == user.BackupEmail;
        }
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(CreatedDate);
            hash.Add(BithDate);
            hash.Add(Age);
            hash.Add(CpfOrCnpj);
            hash.Add(Password);
            hash.Add(Email);
            hash.Add(BackupEmail);
            return hash.ToHashCode();
        }
    }
}
