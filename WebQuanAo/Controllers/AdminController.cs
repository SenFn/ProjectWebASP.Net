using System;
using System.Collections.Generic;
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
        public ActionResult Header()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                return View();

            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
        public ActionResult Body(string b1 = "", string b2 = "", string b3 = "", string b4 = "", string b5 = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                if (b1 == "" ||
                    b2 == "" ||
                    b3 == "" ||
                    b4 == "" ||
                    b5 == "")
                {
                    using (DBStore dbModel = new DBStore())
                    {
                        bodySave bS = dbModel.bodySaves.FirstOrDefault();
                        ViewBag.bodySave = bS;
                    }
                    return View();
                }
                else
                {
                    using (DBStore dbModel = new DBStore())
                    {
                        bodySave bS = dbModel.bodySaves.FirstOrDefault();
                        dbModel.bodySaves.Remove(bS);
                        dbModel.SaveChanges();
                        bS.b1 = b1;
                        bS.b2 = b2;
                        bS.b3 = b3;
                        bS.b4 = b4;
                        bS.b5 = b5;
                        dbModel.bodySaves.Add(bS);
                        dbModel.SaveChanges();
                        ViewBag.bodySave = bS;
                    }
                    return View();
                }



            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public ActionResult ProductInfo()
        {

            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                using (DBStore dbModel = new DBStore())
                {
                    List<product> product = dbModel.products.ToList();
                    List<productInfo> productInfo = dbModel.productInfoes.ToList();
                    ViewBag.productInfo = productInfo;
                    ViewBag.product = product;
                }
                return View();

            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
        public ActionResult EditProduct()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        //Đây Là Form Hóa Đơn Bán
        public ActionResult Bill()
        {
            return View();
        }

        //Đây Là Form Chi Tiết Của Hóa Đơn Bán Khi Click Vào Tình Trạng Của Hóa Đơn
        public ActionResult BillInfo()
        {
            return View();
        }

        //Đây Là Form Danh Sách Tài Khoản (Đã Chỉnh Sửa Chỗ DropDown Không Còn Chia Làm 2 Loại Tài Khoản Nữa Mà Lọc Chung Với Phần Tìm Kiếm
        public ActionResult ListAccount(bool? admin, string name = "", string email = "", string location = "")
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                using (DBStore dbModel = new DBStore())
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


                }
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
           
        }

        public ActionResult RemoveAccount(string acc = "")
        {
            if(acc == "")
                return RedirectToAction("ListAccount", "Admin");

            using (DBStore dbModel = new DBStore())
            {
                account find = dbModel.accounts.FirstOrDefault(a=> a.username == acc && a.admin != true);
                dbModel.accounts.Remove(find);
                dbModel.SaveChanges();
            }

            return RedirectToAction("ListAccount", "Admin");
        }
    }
}