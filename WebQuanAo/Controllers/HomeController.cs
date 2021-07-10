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
                ViewBag.admin = Session["admin"];
            }
            else
            {
                ViewBag.userName = "";
            }

            using (DBStore dbModel = new DBStore())
            {
                List<product> product = dbModel.products.ToList();
                ViewBag.product = product;

                bodySave bS = dbModel.bodySaves.FirstOrDefault();
                ViewBag.bodySave = bS;
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
                Session["admin"] = names.admin;
                return RedirectToAction("Index");
            }

        }


        public ActionResult AddCard(int count, int? id, decimal? price, string name = "", string url = "", string size = "")
        {
            List<GioHang> cart = GetListCart();

            string array = "";
            if (cart.Exists(x => x.id == id && x.size == size))
            {
                GioHang gioHang = cart.Find(x => x.id == id & x.size == size);

                cart.Remove(gioHang);
                gioHang.count += count;
                cart.Add(gioHang);
            }
            else
            {
                cart.Add(new GioHang(id, name, url, size, price, count));
            }

            foreach (GioHang l in cart)
            {
                array += l.ToString() + "\n";
            }
            Session["Cart"] = cart;
            return RedirectToAction("Cart");
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
                ViewBag.admin = Session["admin"];
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
                List<productInfo> pInfo = dbModel.productInfoes.ToList();
                if (product == null)
                {
                    product product1 = dbModel.products.FirstOrDefault(x => x.id == 1);
                    List<productInfo> info1 = pInfo.Where(x => x.id == 1).ToList();
                    ViewBag.product = product1;
                    ViewBag.infoS = info1.Exists(x => x.size.Contains("S")) ? info1.Find(x => x.size.Contains("S")) : new productInfo();
                    ViewBag.infoM = info1.Exists(x => x.size.Contains("M")) ? info1.Find(x => x.size.Contains("M")) : new productInfo();
                    ViewBag.infoL = info1.Exists(x => x.size.Contains("L")) ? info1.Find(x => x.size.Contains("L")) : new productInfo();
                    return View();
                }

                List<productInfo> info = pInfo.Where(x => x.id == id).ToList();
                ViewBag.infoS = info.Exists(x => x.size.Contains("S")) ? info.Find(x => x.size.Contains("S")) : new productInfo();
                ViewBag.infoM = info.Exists(x => x.size.Contains("M")) ? info.Find(x => x.size.Contains("M")) : new productInfo();
                ViewBag.infoL = info.Exists(x => x.size.Contains("L")) ? info.Find(x => x.size.Contains("L")) : new productInfo();
                ViewBag.product = product;

            }
            return View();
        }



        public ActionResult Cart(int? id)
        {
            using (DBStore dbModel = new DBStore())
            {
                List<product> product = dbModel.products.ToList();
                ViewBag.product = product;
            }
            if (id is null)
            {
                List<GioHang> cart1 = GetListCart();
                if (Session["Cart"] is List<GioHang>)
                {
                    ViewBag.Cart = (List<GioHang>)Session["Cart"];
                }
                else
                {
                    ViewBag.Cart = new List<GioHang>();
                }
                return View();
            }
            List<GioHang> cart = GetListCart();
            cart.RemoveAt((int)id);
            Session["Cart"] = cart;
            ViewBag.Cart = (List<GioHang>)Session["Cart"];
            return View();


        }


        public ActionResult LogOut()
        {
            Session["username"] = null;
            Session["admin"] = null;
            return RedirectToAction("Login");
        }


        public ActionResult ComfirmPay()
        {
            return View();
        }

        
        public ActionResult SuccessPay()
        {
            return View();
        }
    }
}