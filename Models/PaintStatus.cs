using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{

    public enum PaintStatusType
    {
        Available = 1,
        RunningLow = 2,
        OutOfStock = 3,

    }

    public enum PaintColourType
    {
        Blue = 1,
        Grey = 2,
        Black = 3,
        White = 4,
        Purple = 5
    }

    public class PaintStatus
    {
        [Key]
        public PaintColourType Color { get; set; } = PaintColourType.Blue;
        public PaintStatusType Status { get; set; } = PaintStatusType.Available;
    }
}
