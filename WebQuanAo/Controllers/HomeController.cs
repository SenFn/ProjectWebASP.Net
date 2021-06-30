using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQuanAo.Models;

namespace WebQuanAo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                ViewBag.userName = Session["username"].ToString();
            }
            else
            {
                ViewBag.userName = "";
            }

            using (DBStore dbModel = new DBStore())
            {
                List<product> product = dbModel.products.ToList();
                ViewBag.product = product;
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public ActionResult SignUp(string username, string password, string verifyPassword, string email, string phone)
        {


            using (DBStore dbModel = new DBStore())
            {
                account acc = new account(username, password, email, phone);
                dbModel.accounts.Add(acc);


                try
                {
                    dbModel.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            ModelState.Clear();


            Session["username"] = username;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (DBStore dbModel = new DBStore())
            {
                account names = dbModel.accounts.FirstOrDefault(x => x.username == username);

                if (names == null)
                {
                    return View();
                }

                if (names.password != password)
                {
                    return View();
                }
                Session["username"] = username;
                return RedirectToAction("Index");
            }

        }


        public ActionResult AddCard(int count, int? id, string name = "", string url = "", string price = "")
        {
            if (id == null)
            {
                return Content("No more id");
            }

            List<GioHang> cart = GetListCart();

            string array = "";
            if (cart.Exists(x => x.id == id))
            {
                GioHang gioHang = cart.Find(x => x.id == id);
                gioHang.count += count;
                cart = cart.Where(rm => rm.id != id).ToList();
                cart.Add(gioHang);
            }
            else
            {
                cart.Add(new GioHang(id, name, url, price, count));
            }

            foreach (GioHang l in cart)
            {
                array += l.ToString() + "\n";
            }
            Session["Cart"] = cart;
            return Content(array);
        }

        private List<GioHang> GetListCart()
        {
            if (Session["Cart"] is List<GioHang>)
            {
                return (List<GioHang>)Session["Cart"];
            }
            else
            {
                return new List<GioHang>();
            }
        }

        public ActionResult Product(int? id)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                ViewBag.userName = Session["username"].ToString();
            }
            else
            {
                ViewBag.userName = "";
            }

            if (id == null)
                id = 1;

            using (DBStore dbModel = new DBStore())
            {
                product product = dbModel.products.FirstOrDefault(x => x.id == id);
                ViewBag.product = product;
            }
            return View();
        }
    }
}