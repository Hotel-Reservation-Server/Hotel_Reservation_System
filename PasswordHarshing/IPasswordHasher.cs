namespace Hotel_Reservation_System.PasswordHarshing
{
    public interface IPasswordHasher
    {
        //method to harsh the password
        string Hash(string password);

        //to verify if the harshed password matches
        bool verify(string passwordHash, string inputPassword);
    }
}
