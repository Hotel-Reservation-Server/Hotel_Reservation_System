namespace Hotel_Reservation_System.Password
{
    public class PasswordValidator
    {
        public bool IsPasswordStrong(string password)
        {
            // Check the minimum length (8 or more characters)
            if (password.Length < 8)
            {
                return false;
            }

            // Check for at least one uppercase letter
            bool hasUpperChar = password.Any(char.IsUpper);

            // Check for at least one lowercase letter
            bool hasLowerChar = password.Any(char.IsLower);

            // 4. Check for at least one digit
            bool hasDigit = password.Any(char.IsDigit);

            // Check for at least one special character
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            // Combine all conditions to ensure they are all met
            return hasUpperChar && hasLowerChar && hasDigit && hasSpecialChar;
        }
    }
}
