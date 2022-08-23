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

        [JsonPropertyOrder(2)]
        [StringLength(50)]
        public string Street { get; set; }

        [JsonPropertyOrder(3)]
        [StringLength(20)]
        public string City { get; set; }

        [JsonPropertyOrder(4)]
        [StringLength(2)]
        public string State { get; set; }

        [JsonPropertyOrder(5)]
        public int Number { get; set; }

        [JsonPropertyOrder(6)]
        [StringLength(50)]
        public string? Complement { get; set; }

        [JsonPropertyOrder(7)]
        [StringLength(50)]
        public string District { get; set; }

        [JsonPropertyOrder(8)]
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
