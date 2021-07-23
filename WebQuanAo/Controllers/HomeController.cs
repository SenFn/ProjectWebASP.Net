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
        public ActionResult SignUp(string username, string password, string verifyPassword, string email, string phone, string area = "")
        {
            ViewBag.Page = "SignUp";
            using (DBStore dbModel = new DBStore())
            {
                //public account()
                //{

                //}
                //public account(string username, string password, string email, string phone)
                //{
                //    this.username = username;
                //    this.password = password;
                //    this.email = email;
                //    this.phone = phone;
                //}

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

            if (area == "ConfirmCart")
                return RedirectToAction("ComfirmPay");
            else if (area == "")
                return RedirectToAction("Index");
            else
                return RedirectToAction(area);
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string area = "")
        {
            ViewBag.Page = "Login";
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
                if (area == "ConfirmCart")
                    return RedirectToAction("ComfirmPay");
                else if (area == "")
                    return RedirectToAction("Index");
                else
                    return RedirectToAction(area);
            }

        }


        public ActionResult AddCard(int count, int? id, decimal price, string name = "", string url = "", string size = "")
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
            ViewBag.Page = "Product";
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
            ViewBag.Page = "Cart";
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
            return RedirectToAction("Cart");


        }


        public ActionResult LogOut()
        {
            Session["username"] = null;
            Session["admin"] = null;
            return RedirectToAction("Login");
        }


        public ActionResult ComfirmPay()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                string uname = Session["username"].ToString();
                ViewBag.userName = uname;
                ViewBag.admin = Session["admin"];

                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == uname);

                    if (names == null)
                    {
                        return View();
                    }
                    ViewBag.accResult = names;
                }
            }
            else
            {
                ViewBag.userName = "";
                return RedirectToAction("Login", "Home", new { area = "ConfirmCart" });
            }

            using (DBStore dbModel = new DBStore())
            {
                List<product> product = dbModel.products.ToList();
                ViewBag.product = product;
            }
            List<GioHang> cart = GetListCart();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Home", new { area = "ConfirmCart" });
            }
            ViewBag.cart = cart;
            return View();
        }


        public ActionResult SuccessPay()
        {
            string name = Session["username"].ToString();
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                ViewBag.userName = Session["username"].ToString();
                ViewBag.admin = Session["admin"];              

                using (DBStore dbModel = new DBStore())
                {
                    DateTime today = DateTime.Today;

                    account acc = dbModel.accounts.FirstOrDefault(x => x.username == name);
                    

                    cart newCart = new cart();
                    newCart.idCart = dbModel.carts.ToList().Count;
                    newCart.id = acc.id;
                    newCart.ngaydathang = today;
                    newCart.ngaygiao = today.AddDays(2);
                    newCart.type = 0;
                    newCart.location = acc.location;
                    dbModel.carts.Add(newCart);
                    dbModel.SaveChanges();
                    

                List<GioHang> gh = (List<GioHang>)Session["Cart"];

                    foreach(GioHang gio in gh)
                    {
                        cardInfo newCardInfo = new cardInfo();
                        newCardInfo.idCart = newCart.idCart;
                        newCardInfo.id = gio.id??0;
                        newCardInfo.price = gio.price;
                        newCardInfo.count = gio.count;
                        newCardInfo.discount = 0;
                        newCardInfo.size = gio.size;
                        dbModel.cardInfoes.Add(newCardInfo);
                        dbModel.SaveChanges();
                    }

                    
                }

                Session["Cart"] = new List<GioHang>();
            }
            else
            {
                ViewBag.userName = "";
            }
            

            return View();
        }

        /* Cập Nhật Thông Tin Tài Khoản */
        public ActionResult UpdateInfo()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                string uName = Session["username"].ToString();

                ViewBag.userName = uName;
                ViewBag.admin = Session["admin"];
                using (DBStore dbModel = new DBStore())
                {
                    account acc = dbModel.accounts.FirstOrDefault(a => a.username == uName);
                    ViewBag.acc = acc;
                }
            }
            else
            {
                ViewBag.userName = "";
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        /* Đổi Mật Khẩu */
        public ActionResult ChangePassword()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                string uName = Session["username"].ToString();

                ViewBag.userName = uName;
                ViewBag.admin = Session["admin"];
                using (DBStore dbModel = new DBStore())
                {
                    account acc = dbModel.accounts.FirstOrDefault(a => a.username == uName);
                    ViewBag.acc = acc;
                }
            }
            else
            {
                ViewBag.userName = "";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult SaveAccount(int id, string name, string email, string phone, string location)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                string uName = Session["username"].ToString();

                ViewBag.userName = uName;
                ViewBag.admin = Session["admin"];
                using (DBStore dbModel = new DBStore())
                {
                    account acc = dbModel.accounts.FirstOrDefault(a => a.username == uName);
                    acc.name = name;
                    acc.email = email;
                    acc.phone = phone;
                    acc.location = location;
                    dbModel.SaveChanges();
                }
            }
            else
            {
                ViewBag.userName = "";
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("UpdateInfo"); ;
        }

        public ActionResult SavePass(int id, string pass)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                string uName = Session["username"].ToString();

                ViewBag.userName = uName;
                ViewBag.admin = Session["admin"];
                using (DBStore dbModel = new DBStore())
                {
                    account acc = dbModel.accounts.FirstOrDefault(a => a.username == uName);
                    acc.password = pass;
                    dbModel.SaveChanges();
                }
            }
            else
            {
                ViewBag.userName = "";
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("UpdateInfo");
        }

        [HttpPost]
        public ActionResult UpdateCart(string id, string count)
        {            
            if (Session["Cart"] is List<GioHang>)
            {
                List<GioHang> cart = (List<GioHang>)Session["Cart"];
                cart[Int32.Parse(id)].count = Int32.Parse(count);
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.Cart = new List<GioHang>();
            }
            return RedirectToAction("Cart");
        }

        /* View Lịch Sử Thanh Toán */
        public ActionResult BilHistory()
        {
            return View();
        }

        /* View Thông Tin Chi Tiết Của Hóa Đơn Trong Lịch Sử Thanh Toán */
        public ActionResult BillInfoHistory()
        {
            return View();
        }
    }
}