namespace PlaceHolder.DTOs
{
    public class TicketCreateByUserDTO
    {
        [MaxLength(1000, ErrorMessage = "Description must have maximum of {1} characters")]
        public string Description { get; set; }

        [MaxLength(30, ErrorMessage = "Category must have maximum of {1} characters")]
        public string Category { get; set; }

        [MaxLength(30, ErrorMessage = "SubCategory must have maximum of {1} characters")]
        public string SubCategory { get; set; }

        [MaxLength(50, ErrorMessage = "Title must have maximum of {1} characters")]
        public string Title { get; set; }

        public TicketCreateByUserDTO()
        {

        }

        public TicketCreateByUserDTO(string description, string category, string subCategory, string title)
        {
            Description = description;
            Category = category;
            SubCategory = subCategory;
            Title = title;
        }
    }
}
