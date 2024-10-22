using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Hotel_Reservation_System.Password_and_Email
{
    public class EmailValidation
    {
        public bool IsValidEmail(string emailAddress)
        {
            string[] tlds = { "com", "org", "net", "edu" };



            //string emailPattern = "/ ^\w + ([\.-] ?\w +)@\w + ([\.-] ?\w +)(\.\w{ 2,3})+$/";
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(emailAddress, emailPattern))
            {
                return false;
            }

            // Additional checks (optional)
            // ...
           

            // Check if the email address has a valid MX record
         /*   using (var client = new System.Net.Mail.SmtpClient())
            {
                try
                {
                    client.SendMailAsync(new MailMessage(emailAddress, "test@example.com", "Test", "Test"));
                }
                catch (SmtpException ex)
                {
                    // Invalid email address
                    Console.WriteLine($"Email validation failed: {ex.Message}");
                    return false;
                }
           }*/
                return true;
        }
    }
 }

