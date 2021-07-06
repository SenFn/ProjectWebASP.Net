using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebQuanAo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Body()
        {
            return View();
        }
        public ActionResult ProductInfo()
        {
            return View();
        }
        public ActionResult EditProduct()
        {
            return View();
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
        public ActionResult ListAccount()
        {
            return View();
        }
    }
}