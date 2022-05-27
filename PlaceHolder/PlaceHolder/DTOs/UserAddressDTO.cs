namespace PlaceHolder.DTOs
{
    public class UserAddressDTO
    {
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
    }
}
