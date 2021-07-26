using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQuanAo.Models;

namespace WebQuanAo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        public ActionResult Header(string b1 = "", string b2 = "", string b3 = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (b1 == "" ||
                               b2 == "" ||
                               b3 == "" )
                        {
                            using (DBStore dbModel1 = new DBStore())
                            {
                                headerSave bS = dbModel.headerSaves.FirstOrDefault();
                                if(bS == null)
                                {
                                    bS = new headerSave();
                                    bS.logo = "../Content/Images/Icon/kisspng-logo-brand-retail-price-book-store-5b1ff6bb22f1a4.9079057215288214351431-removebg-preview.png";
                                    bS.accountIcon = "../Content/Images/Icon/icons8_account_25px.png";
                                    bS.cardIcon = "../Content/Images/Icon/icons8_delivery_handcart_24px.png";
                                    dbModel1.headerSaves.Add(bS);
                                }
                                    
                                ViewBag.headerSave = bS;
                            }
                            return View();
                        }
                        else
                        {
                            using (DBStore dbModel1 = new DBStore())
                            {
                                headerSave bS = dbModel1.headerSaves.FirstOrDefault();
                                dbModel1.headerSaves.Remove(bS);
                                dbModel1.SaveChanges();
                                bS.logo = b1;
                                bS.accountIcon = b2;
                                bS.cardIcon = b3;
                                dbModel1.headerSaves.Add(bS);
                                dbModel1.SaveChanges();
                                ViewBag.headerSave = bS;
                            }
                            return View();
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
        public ActionResult Body(string b1 = "", string b2 = "", string b3 = "", string b4 = "", string b5 = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (b1 == "" ||
                   b2 == "" ||
                   b3 == "" ||
                   b4 == "" ||
                   b5 == "")
                        {
                            using (DBStore dbModel1 = new DBStore())
                            {
                                bodySave bS = dbModel.bodySaves.FirstOrDefault();
                                ViewBag.bodySave = bS;
                            }
                            return View();
                        }
                        else
                        {
                            using (DBStore dbModel1 = new DBStore())
                            {
                                bodySave bS = dbModel1.bodySaves.FirstOrDefault();
                                dbModel1.bodySaves.Remove(bS);
                                dbModel1.SaveChanges();
                                bS.b1 = b1;
                                bS.b2 = b2;
                                bS.b3 = b3;
                                bS.b4 = b4;
                                bS.b5 = b5;
                                dbModel1.bodySaves.Add(bS);
                                dbModel1.SaveChanges();
                                ViewBag.bodySave = bS;
                            }
                            return View();
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public ActionResult ProductInfo()
        {

            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        using (DBStore dbModel1 = new DBStore())
                        {
                            List<product> product = dbModel1.products.ToList();
                            List<productInfo> productInfo = dbModel1.productInfoes.ToList();
                            ViewBag.productInfo = productInfo;
                            ViewBag.product = product;
                        }
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
        public ActionResult EditProduct(int ?id, string size ="")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (id == null || size == "")
                        {
                            ViewBag.Title = "Add Product";
                            return View();
                        }
                        ViewBag.Title = "Edit Product";


                        List<product> product = dbModel.products.ToList();
                            List<productInfo> productInfo = dbModel.productInfoes.ToList();
                            ViewBag.productInfo = productInfo;
                            ViewBag.product = product;


                        using (var db = new DBStore())
                        {
                            var result = db.products.SingleOrDefault(b => b.id == id);
                            var result2 = db.productInfoes.SingleOrDefault(b => b.id == id && b.size == size);
                            if (result != null && result2 != null)
                            {
                                ViewBag.name = result.name;
                                ViewBag.image = result.srcImage;
                                ViewBag.price = result2.price;
                                ViewBag.count = result2.count;
                                ViewBag.size = size;
                                ViewBag.id = id;
                            }
                        }
                    }
                }
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public ActionResult SaveProduct(int ?id, string size, string price, string image, string name, int count)
        {
            decimal p = decimal.Parse(price, CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(Session["username"] as string))
            {

                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (id == null || size == "")
                            return RedirectToAction("ProductInfo", "Admin");

                        product result = dbModel.products.FirstOrDefault(b => b.id == id);

                        if (result.id == id)
                        {
                            result.name = name;
                            result.srcImage = image;                            
                            dbModel.SaveChanges();
                        }

                        productInfo result1 = dbModel.productInfoes.FirstOrDefault(b => b.id == id && b.size == size);
                        if (result is null)
                        {
                            dbModel.productInfoes.Remove(result1);                                                      
                            dbModel.SaveChanges();

                            result1.id = id;
                            result1.size = size;
                            result1.price = p;
                            result1.count = count;
                            dbModel.productInfoes.Add(result1);
                            dbModel.SaveChanges();
                        }
                        else
                        {
                            result1 = new productInfo();
                            result1.id = id;
                            result1.size = size;
                            result1.price = p;
                            result1.count = count;
                            dbModel.productInfoes.Add(result1);
                            dbModel.SaveChanges();
                        }


                        return RedirectToAction("ProductInfo", "Admin");
                    }
                }
                
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        //Đây Là Form Hóa Đơn Bán
        public ActionResult Bill(bool complete = false, bool holding = false, bool priceT = false, bool priceG = false, bool pNew = false, bool pOld = false)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        List<cart> cartList = dbModel.carts.ToList();
                        List<cardInfo> cartI = dbModel.cardInfoes.ToList();
                        ViewBag.pending = "";
                        if (complete == true)
                        {
                            cartList = dbModel.carts.Where(x=>x.type == 1).ToList();
                            ViewBag.pending = "Tình Trạng: Hoàn Thành";
                        }
                        if(holding == true)
                        {
                            cartList = dbModel.carts.Where(x => x.type == 0).ToList();
                            ViewBag.pending = "Tình Trạng: Chưa giao";
                        }

                        if (pNew == true)
                        {
                            cartList = dbModel.carts.OrderByDescending(x => x.ngaydathang).ToList();
                            ViewBag.pending = "Xếp theo ngày mới";
                        }
                        if (pOld == true)
                        {
                            cartList = dbModel.carts.OrderBy(x => x.ngaydathang).ToList();
                            ViewBag.pending = "Xếp theo ngày cũ";
                        }
                        

                        if (priceT == true)
                        {
                            List<SortList> listSort = new List<SortList>();
                            for (int i=0;i< cartList.Count; i++)
                            {
                                decimal total = 0;
                                List<cardInfo> cartF = cartI.Where(x => x.idCart == cartList[i].idCart).ToList();
                                foreach (cardInfo c in cartF)
                                {
                                    total += c.price * c.count;
                                }
                                listSort.Add(new SortList(i, total));
                            }
                            listSort = listSort.OrderBy(x=> x.total).ToList();
                            List<cart> newList = new List<cart>();
                            for (int i = 0; i < listSort.Count; i++)
                            {
                                cart c = cartList.FirstOrDefault(x => x.idCart == listSort[i].id);
                                newList.Add(c);
                            }
                            cartList = newList;

                        }

                        if (priceG == true)
                        {
                            List<SortList> listSort = new List<SortList>();
                            for (int i = 0; i < cartList.Count; i++)
                            {
                                decimal total = 0;
                                List<cardInfo> cartF = cartI.Where(x => x.idCart == cartList[i].idCart).ToList();
                                foreach (cardInfo c in cartF)
                                {
                                    total += c.price * c.count;
                                }
                                listSort.Add(new SortList(i, total));
                            }
                            listSort = listSort.OrderByDescending(x => x.total).ToList();
                            List<cart> newList = new List<cart>();
                            for (int i = 0; i < listSort.Count; i++)
                            {
                                cart c = cartList.FirstOrDefault(x => x.idCart == listSort[i].id);
                                newList.Add(c);
                            }
                            cartList = newList;

                        }





                        ViewBag.carts = cartList;
                        ViewBag.cartsI = cartI;                        
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        //Đây Là Form Chi Tiết Của Hóa Đơn Bán Khi Click Vào Tình Trạng Của Hóa Đơn
        public ActionResult BillInfo(int ?id)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if(id == null)
                        {
                            return RedirectToAction("Bill", "Admin");
                        }
                        List<cardInfo> cartI = dbModel.cardInfoes.Where(x=> x.idCart == id).ToList();                        
                        List<product> pList = dbModel.products.ToList();

                        ViewBag.cartsI = cartI;
                        ViewBag.pList = pList;
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public ActionResult ConfirmBill(int? id)
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Bill", "Admin");
                        }
                        cart cartGet = dbModel.carts.FirstOrDefault(x => x.idCart == id);
                        cartGet.type = 1;
                        dbModel.SaveChanges();
                        return RedirectToAction("Bill", "Admin");
                    }
                }

            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        //Đây Là Form Danh Sách Tài Khoản (Đã Chỉnh Sửa Chỗ DropDown Không Còn Chia Làm 2 Loại Tài Khoản Nữa Mà Lọc Chung Với Phần Tìm Kiếm
        public ActionResult ListAccount(bool? admin, string name = "", string email = "", string location = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    account names = dbModel.accounts.FirstOrDefault(x => x.username == userName);
                    if (names.admin == true)
                    {
                        if (name != "")
                        {
                            List<account> account = dbModel.accounts.Where(a => (a.username.ToLower()).IndexOf(name) != -1).ToList();
                            ViewBag.account = account;
                        }
                        else if (email != "")
                        {
                            List<account> account = dbModel.accounts.Where(a => (a.email.ToLower()).IndexOf(email) != -1).ToList();
                            ViewBag.account = account;
                        }
                        else if (location != "")
                        {
                            List<account> account = dbModel.accounts.Where(a => (a.location.ToLower()).IndexOf(location) != -1).ToList();
                            ViewBag.account = account;
                        }
                        else if (admin == true)
                        {
                            List<account> account = dbModel.accounts.Where(a => a.admin == true).ToList();
                            ViewBag.account = account;
                        }
                        else if (admin == false)
                        {
                            List<account> account = dbModel.accounts.Where(a => a.admin != true).ToList();
                            ViewBag.account = account;
                        }
                        else if (admin == null)
                        {
                            List<account> account = dbModel.accounts.ToList();
                            ViewBag.account = account;
                        }                        
                        return View();
                    }
                }

                
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
           
        }

        public ActionResult RemoveAccount(string acc = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    if (acc == "")
                        return RedirectToAction("ListAccount", "Admin");

                    account find = dbModel.accounts.FirstOrDefault(a => a.username == acc && a.admin != true);
                    dbModel.accounts.Remove(find);
                    dbModel.SaveChanges();               

                    return RedirectToAction("ListAccount", "Admin");
                }

            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
            
        }

        public ActionResult RemoveProDuctinfo(int ?id, string size = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                String userName = Session["username"].ToString();
                using (DBStore dbModel = new DBStore())
                {
                    if (id == null || size == "")
                        return RedirectToAction("ProductInfo", "Admin");

                    productInfo find = dbModel.productInfoes.FirstOrDefault(a => a.id == id && a.size == size);
                    dbModel.productInfoes.Remove(find);
                    dbModel.SaveChanges();

                    return RedirectToAction("ProductInfo", "Admin");
                }

            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
            
        }
    }
}