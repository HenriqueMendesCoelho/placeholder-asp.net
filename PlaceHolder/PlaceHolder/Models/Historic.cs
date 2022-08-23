using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("historical")]
    public class Historic : BaseEntity
    {
        [JsonPropertyOrder(2)]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [JsonPropertyOrder(3)]
        [StringLength(200)]
        public string Text { get; set; }

        [JsonPropertyOrder(4)]
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public Ticket Ticket { get; set; }

        [JsonPropertyOrder(5)]
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
