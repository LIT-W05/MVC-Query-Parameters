using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipRegion { get; set; }
        public DateTime? ShippedDate { get; set; }
    }

    public class OrderDetail
    {
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}