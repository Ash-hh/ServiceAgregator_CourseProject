

namespace ServiceAgregator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_ID { get; set; }

        public int Service_ID { get; set; }

        public int User_ID { get; set; }

        
        [StringLength(25)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Execution { get; set; }

        public virtual Users Users { get; set; }

        public virtual Services Services { get; set; }
       
    }
}
