namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Resim")]
    public partial class Resim
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Link { get; set; }
    }
}
