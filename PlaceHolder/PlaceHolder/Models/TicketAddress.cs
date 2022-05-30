using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("ticket_address")]
    public class TicketAddress : BaseEntity
    {
        [JsonIgnore]
        [ForeignKey("Ticket")]
        [Column("ticket_id")]
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
        public Ticket Ticket { get; set; }

        public TicketAddress() {    }

        public TicketAddress(long id, string street, string city, string state, int number, string? complement, string district, string cep, Ticket ticket)
        {
            Id = id;
            Street = street;
            City = city;
            State = state;
            Number = number;
            Complement = complement;
            District = district;
            Cep = cep;
            Ticket = ticket;
        }
    }
}
