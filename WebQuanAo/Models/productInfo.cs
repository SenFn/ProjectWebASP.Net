namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("productInfo")]
    public partial class productInfo
    {
        public int? id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string size { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int count { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "money")]
        public decimal price { get; set; }

        public virtual product product { get; set; }
    }
}
