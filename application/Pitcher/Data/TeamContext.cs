using Pitcher.Models;
using Microsoft.EntityFrameworkCore;

namespace Pitcher.Data
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations {get;set;}
        public DbSet<Job> Jobs {get;set;}        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<Registration>().ToTable("tblRegistration");
            modelBuilder.Entity<Job>().ToTable("tblJob");
        }
        
        public DbSet<Pitcher.Models.Problem> Problem { get; set; }
    }
}