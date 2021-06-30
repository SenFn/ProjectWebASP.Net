namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string name { get; set; }

        [Required]
        [StringLength(200)]
        public string srcImage { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        [Required]
        [StringLength(10)]
        public string types { get; set; }

        [StringLength(50)]
        public string byHuman { get; set; }
    }
}
