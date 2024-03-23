using PaintStockStatusAPI.Models;

namespace PaintStockStatusAPI.Dtos
{
    public class UpdatePaintInventoryDto
    {
        public PaintColourType Colour { get; set; }
        public int Quantity { get; set; }
    }
}
