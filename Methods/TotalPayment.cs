namespace Hotel_Reservation_System.Methods
{
    public class TotalPayment
    {
        public decimal CalculateTotalPayment(decimal roomRate, int nights)
        {
            return (roomRate * nights);
        }
    }
}
