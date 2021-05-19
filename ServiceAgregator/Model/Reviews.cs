using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace ServiceAgregator.Models
{
    public partial class Reviews
    {
        [Key]
        public int Review_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Review_Date { get; set; }

        public int? Sender_Id { get; set; }

        public int? Recipient_Id { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }
        public double? Mark { get; set; }
        public virtual Users UsersSender { get; set; }

        public virtual Users UsersRecepient { get; set; }
    }
}
