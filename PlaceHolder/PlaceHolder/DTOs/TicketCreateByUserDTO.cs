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

        [Required(ErrorMessage = "Street is required")]
        [MaxLength(50, ErrorMessage = "Street must have maximum of {1} characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(20, ErrorMessage = "City must have maximum of {1} characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(2, ErrorMessage = "State must have maximum of {1} characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "District is required")]
        [MaxLength(50, ErrorMessage = "District must have maximum of {1} characters")]
        public string District { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value for number must be between {1} and {2}")]
        public int Number { get; set; }

        [MaxLength(50, ErrorMessage = "Complement must have maximum of {1} characters")]
        public string? Complement { get; set; }

        public string Cep { get; set; }

        public TicketCreateByUserDTO()
        {

        }

        public TicketCreateByUserDTO(string description, string category, string subCategory, string title, int severity, string street, string city, string state, string district, int number, string? complement, string cep)
        {
            Description = description;
            Category = category;
            SubCategory = subCategory;
            Title = title;
            Severity = severity;
            Street = street;
            City = city;
            State = state;
            District = district;
            Number = number;
            Complement = complement;
            Cep = cep;
        }
    }
}
