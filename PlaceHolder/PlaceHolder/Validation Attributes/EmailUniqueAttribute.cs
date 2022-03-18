namespace PlaceHolder.Validation_Attributes
{
    public class EmailUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null) return null;
            if(value == null) return null;

            var repo = (IUserRepository)validationContext.GetService(typeof(IUserRepository));
            if(repo == null) return null;
            
            if (repo.FindByEmail(value.ToString()) != null) return new ValidationResult("Email already used");
            return ValidationResult.Success;
        }
    }
}
