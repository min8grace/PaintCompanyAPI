using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{
    public class PaintInventory
    {
        [Key]
        public PaintColourType Colour { get; set; }
        public int Quantity { get; set; }

        public PaintStatusType GetPaintStatus
        {
            get
            {
                if (Quantity == 0)
                {
                    return PaintStatusType.OutOfStock;
                }
                else if (Quantity <= 200)
                {
                    return PaintStatusType.RunningLow;
                }
                else
                {
                    return PaintStatusType.Available;
                }
            }
        }

    }
}
