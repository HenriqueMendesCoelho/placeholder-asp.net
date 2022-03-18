namespace PlaceHolder.Validation_Attributes
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //CPF validation
            if (value is null) return false;

            var cpf = value.ToString();

            if (cpf.Equals("99999999999") || cpf.Equals("11111111111") ||
                cpf.Equals("22222222222") || cpf.Equals("33333333333") ||
                cpf.Equals("44444444444") || cpf.Equals("55555555555") ||
                cpf.Equals("66666666666") || cpf.Equals("77777777777") ||
                cpf.Equals("88888888888")) return false;

            if (cpf == null) return false;

            var CharArray = cpf.ToCharArray();
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
                FirstDigitValidation = FirstDigitValidation + ArrayConverted[i] * x;
                x = x - 1;
            }
            FirstDigitValidation = FirstDigitValidation * 10;
            FirstDigitValidation = FirstDigitValidation % 11;

            //Validation of the Second Digit
            int SecondDigitValidation = 0;
            x = 11;

            for (int i = 0; i < 10; i++)
            {
                SecondDigitValidation = SecondDigitValidation + ArrayConverted[i] * x;
                x = x - 1;
                if (i == 9)
                {
                    SecondDigitValidation = SecondDigitValidation * 10;
                    SecondDigitValidation = SecondDigitValidation % 11;
                }
            }

            if(FirstDigitValidation == 10) FirstDigitValidation = 0;
            return (FirstDigitValidation == ArrayConverted[9] && SecondDigitValidation == ArrayConverted[10]) ? true : false;
        }
    }
}
