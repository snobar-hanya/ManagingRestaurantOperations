namespace WebApplication1NotGuid.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        // DB relations
        public User User { get; set; }
    }

}
