using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{
    public class PaintInventory
    {
        [Key]
        public PaintColourType Color { get; set; } = PaintColourType.Blue;
        public int Quantity { get; set; }

        // Navigation property for relationship with PaintStatus
        public PaintStatus PaintStatus { get; set; }
    }
}
