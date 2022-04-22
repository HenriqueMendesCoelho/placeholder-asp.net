namespace PlaceHolder.Validation_Attributes
{
    public class CPFUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null) return null;
            if(value == null) return null;

            var repo = (IUserRepository)validationContext.GetService(typeof(IUserRepository));
            if(repo == null) return null;
            
            if (repo.FindByCPF(value.ToString()) != null) return new ValidationResult("CPF already used");
            return ValidationResult.Success;
        }
    }
}
