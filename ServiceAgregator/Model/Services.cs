namespace ServiceAgregator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Services : IDataErrorInfo
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

        [Required(ErrorMessage = "Вы не указали категория")]
        [StringLength(10)]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Поле описания не заполнено")]
        [StringLength(200)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage ="Поле стоимости на заполнено")]
        [RegularExpression(@"^[0-9]{1,7}(?:.[0-9](?:[0-9])?)?\z", ErrorMessage = "Недопустимые символы в графе стоимости")]
        public decimal? Cost 
        {
            get { return _Cost; }
            set { _Cost = Math.Round(value.Value, 2); }
        }


        [NotMapped]
        private decimal? _Cost;

        [Column(TypeName = "date")]
        public DateTime? Date_OfAdd { get; set; }

        public bool? Available { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        public virtual Tags Tags { get; set; }

        public virtual Users Users { get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();
    }
}
