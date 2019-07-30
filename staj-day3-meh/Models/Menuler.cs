namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menuler")]
    public partial class Menuler
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }
    }
}
