using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunGroupWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RunGroupWebApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

       // public DbSet<State> States { get; set; }
       // public DbSet<City> Cities { get; set; }
    }
}
