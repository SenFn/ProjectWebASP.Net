using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebQuanAo.Controllers
{
    public class SortList
    {
        public int id;
        public decimal total;

        public SortList(int id, decimal total)
        {
            this.id = id;
            this.total = total;
        }
    }
}