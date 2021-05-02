using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderProcessing.Models;
namespace OrderProcessing.Controllers
{
    public class HomeController : Controller
    {
        private DbModels db = new DbModels();

        public ActionResult Index()
        {
            var orders = db.Orders.ToList();
            //List<Order> orderRows = new List<Order>();
            //foreach (Order order in orders)
            //{
            //    string employeeName = String.Format("{0} {1}", order.Employee.FirstName.Trim()
            //                                                , order.Employee.LastName.Trim());
            //    //     if (employeeName == User.Identity.Name)
            //    orderRows.Add(order);
            //}

            //return View(orders.ToList());
            return View(orders);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}