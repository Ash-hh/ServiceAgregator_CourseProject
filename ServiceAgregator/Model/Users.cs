
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ServiceAgregator.Models
{ 
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Orders = new HashSet<Orders>();
            Services = new HashSet<Services>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }

        [Required]
        [StringLength(50)]       
        public string Login { get; set; }

        [Required]
        [StringLength(95, MinimumLength=3, ErrorMessage ="???????????? ?????? ??????")]
        public string Password { get; set; }
     
        [StringLength(20, MinimumLength = 3, ErrorMessage = "???????????? ?????? ?????")]        
        public string User_Name { get; set; }

        public short User_Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Registration { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_LastLogin { get; set; }
        public bool? Active { set; get; }
        public byte[] User_Image { get; set; }

        [NotMapped]
        private double? _rating;
        
        public double? Rating 
        {
            get { return _rating; }
            set { _rating = Math.Round(value.Value, 2); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> ReviewsSender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> ReviewsRecepient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Services> Services { get; set; }
    }
}
