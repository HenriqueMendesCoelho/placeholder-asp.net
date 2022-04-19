using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("historic")]
    public class Historic : BaseEntity
    {

        [StringLength(50)]
        public string text { get; set; }

        [JsonIgnore]
        public Ticket Ticket { get; set; }
        public long TicketId { get; set; }

        public Historic()
        {
        
        }
        public Historic(string text, Ticket ticket, long ticketId)
        {
            this.text = text;
            Ticket = ticket;
            TicketId = ticketId;
        }
    }
}
