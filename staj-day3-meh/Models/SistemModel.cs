namespace staj_day3_meh.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SistemModel : DbContext
    {
        public SistemModel()
            : base("name=Sistem6Model")
        {
        }

        public virtual DbSet<Galeriler> Galerilers { get; set; }
        public virtual DbSet<GaleriResim> GaleriResims { get; set; }
        public virtual DbSet<GaleriTip> GaleriTips { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Resim> Resims { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Galeriler>()
                .HasMany(e => e.GaleriResims)
                .WithRequired(e => e.Galeriler)
                .WillCascadeOnDelete(false);
        }
    }
}
