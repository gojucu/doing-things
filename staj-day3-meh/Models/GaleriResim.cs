namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GaleriResim")]
    public partial class GaleriResim
    {
        public int Id { get; set; }

        [Required]
        public string Link { get; set; }

        public int GalerilerID { get; set; }

        public virtual Galeriler Galeriler { get; set; }
    }
}
