using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Backlog.Models
{
    [Table("user")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("full_name")]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Column("cpf")]
        [StringLength(11)]
        public string Cpf { get; set; } = string.Empty;
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Column("address")]
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;
        [Column("backup_email")]
        [StringLength(100)]
        public string BackupEmail { get; set; } = string.Empty;
        public List<Ticket> Ticket { get; set; }
        
        public User()
        {
        
        }

        public User(string fullName, string cpf, string password, string email, string address, string backupEmail, List<Ticket> ticket)
        {
            FullName = fullName;
            Cpf = cpf;
            Password = password;
            Email = email;
            Address = address;
            BackupEmail = backupEmail;
            Ticket = ticket;
        }
    }
}
