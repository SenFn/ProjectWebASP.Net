namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bodySave")]
    public partial class bodySave
    {
        public int id { get; set; }

        [StringLength(500)]
        public string b1 { get; set; }

        [StringLength(500)]
        public string b2 { get; set; }

        [StringLength(500)]
        public string b3 { get; set; }

        [StringLength(500)]
        public string b4 { get; set; }

        [StringLength(500)]
        public string b5 { get; set; }
    }
}
