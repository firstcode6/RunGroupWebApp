using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunGroupWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace RunGroupWebApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //try
            //{
            //    var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (dbCreator != null)
            //    {
            //        if (!dbCreator.CanConnect()) dbCreator.Create();
            //        if (!dbCreator.HasTables()) dbCreator.CreateTables();
            //    }
            //}
            //catch (Exception ex) 
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
