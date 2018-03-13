using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.Properties;

namespace WebApplication7.Controllers
{
    public class NorthwindController : Controller
    {
        public ActionResult Orders()
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            OrdersWithDate ordersWithDate = new OrdersWithDate
            {
                Orders = manager.GetOrders(),
                CurrentDate = DateTime.Now
            };
            return View(ordersWithDate);
        }

        public ActionResult OrderDetails()
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            return View(manager.GetOrderDetailsFor1997());
        }

        public ActionResult DetailsForOrder(int orderId)
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            IEnumerable<OrderDetail> details = manager.GetDetailsForOrder(orderId); 
            return View(details);
        }

        public ActionResult Categories()
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            return View(manager.GetCategories());
        }

        public ActionResult Products(int catId)
        {
            NorthwindManager manager = new NorthwindManager(Settings.Default.ConStr);
            IEnumerable<Product> products = manager.GetProducts(catId);
            ProductsAndCategoryName data = new ProductsAndCategoryName
            {
                Products = products,
                CurrentCategoryName = manager.GetCategoryName(catId)
            };
            return View(data);
        }
    }

    //create an application that has a page where all categories from the northwind
    //database are displayed. The category id of each category should be a link
    //that when clicked takes you to a page that displays all products for that category

    //On top of that table (on the products page) there should be an H1 that says
    // "Products for {category name}".
}