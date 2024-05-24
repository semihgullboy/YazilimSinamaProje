using Microsoft.EntityFrameworkCore;

namespace YazılımSınamaProje.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-DGQ62JR\\SQLEXPRESS;database=yazilimsinamadb; integrated security=true; TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Work> Works { get; set; }

    }
}
