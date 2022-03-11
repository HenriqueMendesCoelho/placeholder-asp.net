using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("user_address")]
    public class UserAddress
    {
        [ForeignKey("User")]
        public long UserAddressId { get; set; }
        [StringLength(50)]
        public string Street { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
        public int Number { get; set; }
        [StringLength(50)]
        public string Complement { get; set; }
        public int Cep { get; set; }
        public User User { get; set; }

        public UserAddress()
        {

        }

        public UserAddress(string street, string city, string state, int number, string complement, int cep)
        {
            Street = street;
            City = city;
            State = state;
            Number = number;
            Complement = complement;
            Cep = cep;
        }
    }
}
