using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderProcessing.Models;
using PagedList;

namespace OrderProcessing.Controllers
{
    public class OrderController : Controller
    {
      
        private DbModels db = new DbModels();
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingDate = Sorting_Order == "Date_Enroll" ? "Date_Description" : "Date";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var orders = from ord in db.Orders select ord;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                orders = orders.Where(ord => ord.Employee.FirstName.ToUpper().Contains(Search_Data.ToUpper())
                    || ord.Employee.LastName.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    orders = orders.OrderByDescending(ord => ord.Employee.FirstName);
                    break;
                case "Date_Enroll":
                    orders = orders.OrderBy(ord => ord.OrderDate);
                    break;
                case "Date_Description":
                    orders = orders.OrderByDescending(ord => ord.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(ord => ord.Employee.FirstName);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(orders.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        public ActionResult Create()
        {
        
           
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName");
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName");
            ViewBag.Products = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }
        [HttpPost]
        public JsonResult SaveOrder(OrderVM O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (DbModels dc = new DbModels())
                {
                    Order order = new Order { CustomerID = O.CustomerID, EmployeeID=O.EmployeeID, ShippedDate=O.ShippedDate, OrderDate = O.OrderDate, ShipName = O.ShipName, ShipAddress=O.ShipAddress, ShipVia=O.ShipVia };
                    foreach (var i in O.OrderDetails)
                    {
                        //
                        // i.TotalAmount = 
                        order.Order_Details.Add(i);
                    }
                    dc.Orders.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
        static public string getOrderTotal(ICollection<Order_Detail> orderDetails)
        {
            decimal orderTotal = 0;
            foreach (Order_Detail item in orderDetails)
            {
                orderTotal += item.UnitPrice * item.Quantity * ((decimal)(1 - item.Discount));
            }

            string returnVal = string.Format("{0:C2}", orderTotal);
            return returnVal;
        }
       
    }
}