bool running = true;

Console.WriteLine("Welcome to the Password Validator");
Console.WriteLine($"Your password must be betweenb {PasswordValidator.MIN_PASSWORD_LENGTH} to {PasswordValidator.MAX_PASSWORD_LENGTH} characters ");
Console.WriteLine($"Your password must contain {PasswordValidator.MIN_UPPERCASE_LETTER} uppercase letters, {PasswordValidator.MIN_LOWERCASE_LETTER} lowercase letters and {PasswordValidator.MIN_DIGIT} digits");
Console.WriteLine($"Your password can not contain the following characters: {string.Join(", ", PasswordValidator.UNALLOWED_CHARCTERS)}");
while (running) {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("\nInput your password to validate: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    string input = Console.ReadLine() ?? " ";

    if(PasswordValidator.IsValid(input))
    { 
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Your password is valid :)");
    } else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Your password is invalid :(");
    }
    Console.ResetColor();

    Console.WriteLine("\nPress any key to continue or 'x' to exit");
    ConsoleKeyInfo key = Console.ReadKey();
    if(key.Key == ConsoleKey.X) {
        break;
    }
}




public static class PasswordValidator
{
    public readonly static int MIN_PASSWORD_LENGTH = 6;
    public readonly static int MAX_PASSWORD_LENGTH = 13;
    public readonly static int MIN_UPPERCASE_LETTER = 1;
    public readonly static int MIN_LOWERCASE_LETTER = 1;
    public readonly static int MIN_DIGIT = 1;
    public readonly static char[] UNALLOWED_CHARCTERS = new char[] { 'T','&' };

    public static bool IsValid(string password) {
        if (password.Length >= MIN_PASSWORD_LENGTH && password.Length <= MAX_PASSWORD_LENGTH) 
        {
            int uppercaseLetters = 0;
            int lowercaseLetters = 0;
            int digits = 0;

            if (UNALLOWED_CHARCTERS.Any(password.Contains))
            {
                Console.WriteLine("Password contains invalid character(s)");
                return false;
            }

            foreach (char letter in password)
            {
                if(char.IsUpper(letter))
                {
                    uppercaseLetters++;
                }
                else if(char.IsLower(letter)) 
                {
                    lowercaseLetters++;
                }
                else if (char.IsDigit(letter)) 
                {
                    digits++;
                }
            }

            if (uppercaseLetters >= MIN_UPPERCASE_LETTER && lowercaseLetters >= MIN_LOWERCASE_LETTER && digits >= MIN_DIGIT)
            {
                Console.WriteLine("Password is valid");
                return true;
            } else {
                Console.WriteLine($"Password must contain {MIN_UPPERCASE_LETTER} uppercase letters, {MIN_LOWERCASE_LETTER} lowercase letters and {MIN_DIGIT} digits");
                return false;
            }
        } else {
            Console.WriteLine($"Password must be within {MIN_PASSWORD_LENGTH} to {MAX_PASSWORD_LENGTH} characters");
            return false;
        }
    }
}