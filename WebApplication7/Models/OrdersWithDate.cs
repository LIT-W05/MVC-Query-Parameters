using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class OrdersWithDate
    {
        public IEnumerable<Order> Orders { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}