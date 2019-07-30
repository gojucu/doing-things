namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuTip")]
    public partial class MenuTip
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }
    }
}
