namespace PlaceHolder.Validation_Attributes
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //CPF validation
            if (value is null) return false;

            var cpf = value.ToString();

            if (cpf is null) return false;

            if (cpf.Equals("11111111111") || cpf.Equals("22222222222") ||
                cpf.Equals("33333333333") || cpf.Equals("44444444444") ||
                cpf.Equals("55555555555") || cpf.Equals("66666666666") ||
                cpf.Equals("77777777777") || cpf.Equals("88888888888") ||
                cpf.Equals("99999999999")) return false;

            var CharArray = cpf.ToCharArray();

            //Validation size of cpf
            if (CharArray.Length < 11) return false;

            var ArrayConverted = new List<int>();

            //Converting char to int
            foreach (char c in CharArray)
            {
                ArrayConverted.Add(int.Parse(c.ToString()));
            }

            //Validation of the First Digit
            int FirstDigitValidation = 0;
            int x = 10;
            for (int i = 0; i < 9; i++)
            {
                FirstDigitValidation += ArrayConverted[i] * x;
                x--;
            }
            FirstDigitValidation *= 10;
            FirstDigitValidation %= 11;

            //Validation of the Second Digit
            int SecondDigitValidation = 0;
            x = 11;

            for (int i = 0; i < 10; i++)
            {
                SecondDigitValidation += ArrayConverted[i] * x;
                x--;
            }
            SecondDigitValidation *= 10;
            SecondDigitValidation %= 11;

            if (FirstDigitValidation == 10) FirstDigitValidation = 0;
            return (FirstDigitValidation == ArrayConverted[9] && SecondDigitValidation == ArrayConverted[10]) ? true : false;
        }
    }
}
