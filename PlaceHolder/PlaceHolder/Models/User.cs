using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace PlaceHolder.Models
{
    [Table("user")]
    [Index(nameof(Cpf), IsUnique = true)]
    public class User : BaseEntity
    {
        [Column("full_name")]
        [StringLength(50)]
        public string FullName { get; set; }
        [Column("cpf")]
        [StringLength(11)]
        public string Cpf { get; set; }
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("backup_email")]
        [StringLength(100)]
        public string BackupEmail { get; set; }
        public UserAddress Address { get; set; }
        public List<Ticket> Ticket { get; set; }

        
        public User()
        {
        
        }

        public User(string fullName, string cpf, string password, string email, UserAddress address, string backupEmail, List<Ticket> ticket)
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
