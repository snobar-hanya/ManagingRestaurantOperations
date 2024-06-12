namespace WebApplication1NotGuid.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // DB relations
        public List<Order> Orders { get; set; }
    }
}
