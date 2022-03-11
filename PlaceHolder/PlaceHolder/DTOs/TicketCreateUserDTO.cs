namespace PlaceHolder.DTOs
{
    public class TicketCreateUserDTO
    {
        [MaxLength(1000, ErrorMessage = "Description must have maximum of 1000 characters")]
        public string Description { get; set; }
        [MaxLength(30, ErrorMessage = "Category must have maximum of 30 characters")]
        public string Category { get; set; }
        [MaxLength(30, ErrorMessage = "SubCategory must have maximum of 30 characters")]
        public string SubCategory { get; set; }
        [MaxLength(50, ErrorMessage = "Title must have maximum of 30 characters")]
        public string Title { get; set; }

        public TicketCreateUserDTO()
        {

        }

        public TicketCreateUserDTO(string description, string category, string subCategory, string title)
        {
            Description = description;
            Category = category;
            SubCategory = subCategory;
            Title = title;
        }
    }
}
