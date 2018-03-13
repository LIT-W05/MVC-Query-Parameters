using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.Properties;

namespace WebApplication7.Controllers
{
    public class SampleController : Controller
    {
        public ActionResult Index(string foo, string bar)
        {
            return View();
        }

        public ActionResult ShowForm()
        {
            return View();
        }

        public ActionResult Search(string searchText)
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            IEnumerable<Product> products = manager.SearchProducts(searchText);
            SearchResults results = new SearchResults
            {
                SearchText = searchText,
                Products = products
            };
            return View(results);
        }

        public ActionResult SearchComplete(string searchText)
        {
            if (String.IsNullOrEmpty(searchText))
            {
                return View(new SearchResults());
            }
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            IEnumerable<Product> products = manager.SearchProducts(searchText);
            SearchResults results = new SearchResults
            {
                SearchText = searchText,
                Products = products
            };
            return View(results);
        }
    }
}