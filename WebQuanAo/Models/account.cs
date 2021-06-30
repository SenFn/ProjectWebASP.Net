namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        public account()
        {
        }

        public account(string username, string password, string email, string phone)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.phone = phone;
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string password { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string email { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(100)]
        public string location { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool admin { get; set; }
    }
}
