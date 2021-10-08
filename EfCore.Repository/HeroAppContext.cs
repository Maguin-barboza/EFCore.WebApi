using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Repository
{
	public class HeroAppContext: DbContext
    {

        public HeroAppContext(DbContextOptions<HeroAppContext> options) :base(options) { } 
        
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity => {
                entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
            });
        }
    }
}