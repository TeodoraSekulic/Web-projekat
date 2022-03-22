using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FirmaContext : DbContext
    {
        public DbSet<Firma> Firme { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Iznajmljivanje> Iznajmljivanja { get; set; }
        public FirmaContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}