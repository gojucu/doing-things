namespace staj_day3_meh.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SistemModel : DbContext
    {
        public SistemModel()
            : base("name=Sistem3Model")
        {
        }

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Resim> Resims { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
