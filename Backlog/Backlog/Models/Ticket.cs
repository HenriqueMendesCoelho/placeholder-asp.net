using System.ComponentModel.DataAnnotations.Schema;

namespace Backlog.Models
{
    [Table("ticket")]
    public class Ticket
    {

        public long Id { get; set; }
        public string Description { get; set; }
        [StringLength(30)]
        public string Category { get; set; }
        [StringLength(30)]
        public string SubCategory { get; set; }
        [StringLength(30)]
        public string Status { get; set; }
        [StringLength(50)]
        public string Responsible { get; set; }
        [StringLength(50)]
        public string Employee { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public long UserId { get; set; }
        public List<Historic> Historics { get; set; }

        public Ticket()
        {

        }
        public Ticket(string description, string category, 
            string subCategory, string status, string responsible, 
            string employee, string title, User user, long userId, List<Historic> historics)
        {
            Description = description;
            Category = category;
            SubCategory = subCategory;
            Status = status;
            Responsible = responsible;
            Employee = employee;
            Title = title;
            User = user;
            UserId = userId;
            Historics = historics;
        }
    }
}
