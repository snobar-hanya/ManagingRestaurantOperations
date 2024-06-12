namespace WebApplication1NotGuid.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int MenuItemID { get; set; }
        public int Quantity { get; set; }

        // DB relations
        public User User { get; set; }
        public MenuItem MenuItem { get; set; }
    }

}
