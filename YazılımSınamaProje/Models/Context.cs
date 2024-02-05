using Microsoft.EntityFrameworkCore;

namespace YazılımSınamaProje.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-DGQ62JR\\SQLEXPRESS;database=yazılımsınamadb; integrated security=true; TrustServerCertificate=True");
        }

        public DbSet<User> users { get; set; }

        public DbSet<Project> projects { get; set; }

        public DbSet<Offer> offers { get; set; }

        public DbSet<Work> works { get; set; }

    }
}
