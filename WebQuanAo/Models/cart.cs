namespace WebQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cart")]
    public partial class cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cart()
        {
            cardInfo = new HashSet<cardInfo>();
        }

        [Key]
        [StringLength(10)]
        public string inCart { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaydathang { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaygiao { get; set; }

        [Required]
        [StringLength(100)]
        public string location { get; set; }

        public virtual account account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cardInfo> cardInfo { get; set; }
    }
}
