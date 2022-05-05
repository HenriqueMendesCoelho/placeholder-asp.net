using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("historical")]
    public class Historic : BaseEntity
    {
        [StringLength(50)]
        public string CreateBy { get; set; }

        [StringLength(200)]
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public Ticket Ticket { get; set; }

        public long TicketId { get; set; }

        public Historic() { }

        public Historic(string createBy, string text, DateTime creationDate, Ticket ticket, long ticketId)
        {
            CreateBy = createBy;
            this.Text = text;
            CreationDate = creationDate;
            Ticket = ticket;
            TicketId = ticketId;
        }
    }
}
