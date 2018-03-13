using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class ProductsAndCategoryName
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategoryName { get; set; }
    }
}