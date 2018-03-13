using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class SearchResults
    {
        public string SearchText { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}