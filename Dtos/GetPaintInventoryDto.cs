using PaintStockStatusAPI.Models;

namespace PaintStockStatusAPI.Dtos
{
    public class GetPaintInventoryDto
    {
        public PaintColourType Colour { get; set; }
        public int Quantity { get; set; }
        public PaintStatusType PaintStatus { get; set; }
    }
}
