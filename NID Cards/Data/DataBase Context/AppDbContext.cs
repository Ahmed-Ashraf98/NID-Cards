using Microsoft.EntityFrameworkCore;
using NID_Cards.Models;

namespace NID_Cards.Data.DataBase_Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Citizen> Citizens { get; set; }   
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<BirthSite> BirthSites { get; set; }
    }
}
