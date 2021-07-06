namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("headerSave")]
    public partial class headerSave
    {
        public int id { get; set; }

        [StringLength(500)]
        public string logo { get; set; }

        [StringLength(500)]
        public string accountIcon { get; set; }

        [StringLength(500)]
        public string cardIcon { get; set; }
    }
}
