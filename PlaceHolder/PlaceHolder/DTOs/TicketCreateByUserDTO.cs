namespace PlaceHolder.DTOs
{
    public class TicketCreateByUserDTO
    {
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1000, ErrorMessage = "Description must have maximum of {1} characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [MaxLength(30, ErrorMessage = "Category must have maximum of {1} characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "SubCategory is required")]
        [MaxLength(30, ErrorMessage = "SubCategory must have maximum of {1} characters")]
        public string SubCategory { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title must have maximum of {1} characters")]
        public string Title { get; set; }

        [Range(1, 10, ErrorMessage = "Value for number must be between {1} and {2}")]
        public int Severity { get; set; }

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
