using Pitcher.Models;
using Microsoft.EntityFrameworkCore;
using Pitcher.Models.TeamViewModels;

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

        public DbSet<Problem> Problems { get; set; }   

        public DbSet<Result> Results {get;set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<Registration>().ToTable("tblRegistration");
            modelBuilder.Entity<Job>().ToTable("tblJob");
            modelBuilder.Entity<Problem>().ToTable("tblProblem");
            modelBuilder.Entity<Chat>().ToTable("tblChat");
            modelBuilder.Entity<Result>().ToTable("tblResult");

            // modelBuilder.Entity<Result>()
                // .HasKey(bc => new { bc.JobID, bc.ProblemID });
            modelBuilder.Entity<Result>()
                .HasOne(bc => bc.Job)
                .WithMany(b => b.Results)
                .HasForeignKey(bc => bc.JobID);
            modelBuilder.Entity<Result>()
                .HasOne(bc => bc.Problem)
                .WithMany(c => c.Result)
                .HasForeignKey(bc => bc.ProblemID);
        }        
    }
}