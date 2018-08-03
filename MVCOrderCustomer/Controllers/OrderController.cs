using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOrderCustomer.Models;


namespace MVCOrderCustomer.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        OrderCustomerEntities db = new OrderCustomerEntities();
        [HttpGet]
        public ActionResult AddOrder()
        {
            var data = new SelectList(db.tblCustomers, "CustomerID", "CustomerName");
            Session["Data"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(string Command)
        {
            tblOrder ob = new tblOrder();

            if (Command == "Save")
            {
                ob.ProductName = Request.Form["txtProductName"].ToString();
                ob.Price = Decimal.Parse(Request.Form["txtPrice"].ToString());
                ob.CustomerID = int.Parse(Request.Form["ddlCustomer"].ToString());
                db.tblOrders.Add(ob);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    ModelState.AddModelError("", "Order Added");
                }
                else
                {
                    ModelState.AddModelError("", "Could not Add Order");
                }

                return View();

            }

            if (Command == "Cancel")
                return View();

            return View();

        }

        [HttpGet]
        public ActionResult GetOrderByName()
        {
            var data = new SelectList(db.tblCustomers, "CustomerID", "CustomerName");
            Session["Data1"] = data;
            return View();

        }

        [HttpPost]
        public ActionResult GetOrderByName(string Show)
        {

            if (Show == "ShowOrder")
            {
                int id = int.Parse(Request.Form["ddlCustomer"].ToString());
                var data = (db.tblOrders.Where(x => x.CustomerID == id)).ToList();

                Session["Data2"] = data;

                return View();
            }

            return View();

        }

        [HttpGet]

        public ActionResult Modify(int id)
        {
            var data = db.tblOrders.Where(x => x.OrderID == id).SingleOrDefault();
            Session["Data3"] = data;
            return View();
        }

        [HttpPost]

        public ActionResult Modify()
        {
            int id = int.Parse(Request.Form["txtid"].ToString());
            var olddata = db.tblOrders.Where(x => x.OrderID == id).SingleOrDefault();
            olddata.Price = Decimal.Parse(Request.Form["price"].ToString());


            var res = db.SaveChanges();
            if (res > 0)
            {
                ModelState.AddModelError("", "Order Modified");

            }

            return RedirectToAction("Home");

           
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetOrderByID()
        {
           
            return View();

        }

        [HttpPost]
        public ActionResult GetOrderByID(string Show)
        {

            if (Show == "ShowOrder")
            {
                int id = int.Parse(Request.Form["id"].ToString());
                var data = (db.tblOrders.Where(x => x.CustomerID == id)).ToList();

                Session["Data5"] = data;

                return View();
            }

            return View();

        }

        [HttpGet]

        public ActionResult GetCustomer()
        {
            return View();
        }

        [HttpPost]

        public ActionResult GetCustomer(string Show)
        {
            if(Show == "ADD")
            {
                tblCustomer n = new tblCustomer();
                n.CustomerName=Request.Form["name"].ToString();
                db.tblCustomers.Add(n);
                var res = db.SaveChanges();

                if (res > 0)
                {
                    ModelState.AddModelError("", "Customer Added! Customer ID :"+n.CustomerID);
                }
                else
                {
                    ModelState.AddModelError("", "Could not Add Customer");
                }

                return View();
            }
            return View();
        }


    }
}