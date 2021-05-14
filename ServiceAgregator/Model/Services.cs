namespace ServiceAgregator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Services()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Service_ID { get; set; }

        public int User_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Tag { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_OfAdd { get; set; }

        public bool? Available { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        public virtual Users Users { get; set; }
    }
}
