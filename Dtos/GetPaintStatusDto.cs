using PaintStockStatusAPI.Models;

namespace PaintStockStatusAPI.Dtos
{
    public class GetPaintStatusDto
    {
        public PaintColourType Color { get; set; }
        public PaintStatusType Status { get; set; }
    }
}
