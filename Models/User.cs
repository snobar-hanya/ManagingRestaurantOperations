namespace WebApplication1NotGuid.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // DB relations
        public List<Reservation> Reservations { get; set; }
        public List<Order> Orders { get; set; }
    }
}
