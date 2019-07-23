namespace staj_day3_meh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urunler")]
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            GaleriResims = new HashSet<GaleriResim>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        public double Fiyat { get; set; }

        public bool Aktif { get; set; }

        [Required]
        [StringLength(500)]
        public string Aciklama { get; set; }

        [Required]
        public string UzunAcıklama { get; set; }

        public bool Silindi { get; set; }

        [Required]
        [StringLength(50)]
        public string Renk { get; set; }

        [Required]
        [StringLength(50)]
        public string Beden { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GaleriResim> GaleriResims { get; set; }
    }
}
