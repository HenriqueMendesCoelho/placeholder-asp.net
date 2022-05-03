using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("users_address")]
    public class UserAddress : BaseEntity
    {
        [ForeignKey("User")]
        [Column("user_id")]
        override
        public long Id { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        public int Number { get; set; }

        [StringLength(50)]
        public string? Complement { get; set; }

        [StringLength(50)]
        public string District { get; set; }

        [StringLength(10)]
        public string Cep { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public UserAddress()
        {

        }

        public UserAddress(long id, string street, string city, string state, int number, string? complement, string district, string cep, User user)
        {
            Id = id;
            Street = street;
            City = city;
            State = state;
            Number = number;
            Complement = complement;
            District = district;
            Cep = cep;
            User = user;
        }
    }
}
