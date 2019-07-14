namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        public bool Aktif { get; set; }

        public int ustid { get; set; }

        public int sira { get; set; }

        [Required]
        [StringLength(500)]
        public string icerik { get; set; }

        [StringLength(500)]
        public string dislink { get; set; }
    }
}
