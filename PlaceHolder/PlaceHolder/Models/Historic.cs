using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("historical")]
    public class Historic : BaseEntity
    {

        [StringLength(50)]
        public string text { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public Ticket Ticket { get; set; }
        public long TicketId { get; set; }

        public Historic() { }

        public Historic(string text, DateTime creationDate, Ticket ticket, long ticketId)
        {
            this.text = text;
            CreationDate = creationDate;
            Ticket = ticket;
            TicketId = ticketId;
        }
    }
}
