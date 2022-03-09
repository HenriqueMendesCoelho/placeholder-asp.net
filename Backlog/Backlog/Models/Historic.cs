namespace Backlog.Models
{
    public class Historic
    {
        public long Id { get; set; }
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
