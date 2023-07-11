using BackTogether.Models;
using Microsoft.EntityFrameworkCore;

namespace BackTogether.Data {
    public class BackTogetherContext : DbContext {
        public BackTogetherContext(DbContextOptions<BackTogetherContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Backing> Backings { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        //
        // No need for this, we inject the configuration in Program.cs
        //
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        //    // WARDNING !! Hard coded connection string is bad and cannot be pushed to repo
        //    optionsBuilder.UseSqlServer(@"BackTogetherDatabase");
        //}
    }
}
