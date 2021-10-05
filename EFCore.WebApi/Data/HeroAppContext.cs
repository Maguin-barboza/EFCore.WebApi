using EFCore.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebApi.Data
{
    public class HeroAppContext: DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MAGNO-PC\\MSSQL2012ENT;Initial Catalog=HeroApp;User ID=sistema;Password=schwer_wissen;Connect Timeout=0;Application Name=EFCore_WebApi");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity => {
                entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
            });
        }
    }
}