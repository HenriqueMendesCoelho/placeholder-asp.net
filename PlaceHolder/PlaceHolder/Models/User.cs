using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace PlaceHolder.Models
{
    [Table("user")]
    [Index(nameof(Cpf), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        [JsonPropertyOrder(2)]
        [Column("full_name")]
        [StringLength(50)]
        public string FullName { get; set; }

        [JsonPropertyOrder(3)]
        [Column("cpf")]
        [StringLength(11)]
        public string Cpf { get; set; }

        [JsonPropertyOrder(4)]
        [Column("password")]
        [StringLength(500)]
        public string Password { get; set; }

        [JsonPropertyOrder(5)]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [JsonPropertyOrder(6)]
        public UserAddress Address { get; set; }

        [JsonPropertyOrder(7)]
        [Column("refresh_token")]
        [StringLength(500)]
        public string? RefreshToken { get; set; }

        [JsonPropertyOrder(8)]
        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [JsonPropertyOrder(9)]
        public DateTime CreationDate { get; set; }

        [JsonPropertyOrder(10)]
        public Profiles.ProfilesEnum profile { get; set; }

        [JsonPropertyOrder(11)]
        public List<Ticket> Ticket { get; set; }

        public User() { }

        public User(string fullName, string cpf, string password, string email,
            UserAddress address, List<Ticket> ticket, string? refreshToken, 
            DateTime? refreshTokenExpiryTime, DateTime creationDate, Profiles.ProfilesEnum profile)
        {
            FullName = fullName;
            Cpf = cpf;
            Password = password;
            Email = email;
            Address = address;
            Ticket = ticket;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
            CreationDate = creationDate;
            this.profile = profile;
        }
    }
}
