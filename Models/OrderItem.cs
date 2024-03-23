namespace PaintStockStatusAPI.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public PaintColourType Color { get; set; } = PaintColourType.Blue;
        public int Quantity { get; set; }

        // Navigation property for relationship with Order
        public Order Order { get; set; }
    }
}
