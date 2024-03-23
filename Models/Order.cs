using System;
using System.Collections.Generic;

namespace PaintStockStatusAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
