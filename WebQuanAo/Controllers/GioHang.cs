namespace WebQuanAo.Controllers
{
    internal class GioHang
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string price { get; set; }
        public int count { get; set; }

        public GioHang(int? id, string name, string url, string price, int count)
        {
            this.id = id;
            this.name = name;
            this.url = url;
            this.price = price;
            this.count = count;
        }

        public override string ToString()
        {
            return id + "|" + name + "|" + url + "|" + price + "|" + count;
        }
    }
}