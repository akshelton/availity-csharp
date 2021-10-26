namespace LISPValidation
{
    public static class ValidationLogic
    {
        public static bool Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var inputChars = input.ToCharArray();
            // Counter for the number of opened and closed parentheses
            var parenCount = 0;

            foreach (var c in inputChars)
            {
                if (c == '(')
                {
                    parenCount++;
                }

                if (c == ')')
                {
                    parenCount--;
                }

                // More closed parens than open
                if (parenCount < 0)
                {
                    return false;
                }
            }
            
            return parenCount == 0;
        }
    }
}