using PlaceHolder.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models
{
    [Table("tickets")]
    public class Ticket : BaseEntity
    {
        [JsonPropertyOrder(2)]
        public string Description { get; set; }

        [JsonPropertyOrder(3)]
        [StringLength(30)]
        public string Category { get; set; }

        [JsonPropertyOrder(4)]
        [StringLength(30)]
        public string SubCategory { get; set; }

        [JsonPropertyOrder(5)]
        [StringLength(50)]
        public string? Responsible { get; set; }

        [JsonPropertyOrder(6)]
        [StringLength(50)]
        public string? Employee { get; set; }

        [JsonPropertyOrder(7)]
        [StringLength(50)]
        public string Title { get; set; }

        [JsonPropertyOrder(8)]
        public int Severity { get; set; }

        [JsonPropertyOrder(9)]
        public DateTime? CreationDate { get; set; }

        [JsonPropertyOrder(10)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status.StatusEnum Status { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonPropertyOrder(11)]
        public TicketAddress Address { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        [JsonPropertyOrder(12)]
        public List<Historic>? Historical { get; set; }

        public Ticket() { }

        public Ticket(string description, string? category, string? subCategory, string? responsible, string? employee, string title, int severity, DateTime? creationDate, Status.StatusEnum status, User user, TicketAddress address, long userId, List<Historic>? historical)
        {
            Description = description;
            Category = category;
            SubCategory = subCategory;
            Responsible = responsible;
            Employee = employee;
            Title = title;
            Severity = severity;
            CreationDate = creationDate;
            Status = status;
            User = user;
            Address = address;
            UserId = userId;
            Historical = historical;
        }
    }
}
