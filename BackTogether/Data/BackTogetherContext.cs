using BackTogether.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Metadata;

namespace BackTogether.Data {
    public class BackTogetherContext : DbContext {
        public BackTogetherContext(DbContextOptions<BackTogetherContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Backing> Backings { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        // WARNING! Could break things, keep only if it doesnt break the DB
        // Make tables have singular names instead of plural
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
            configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
        }

        /*
         * No need for this, we inject the configuration in Program.cs
         */

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //
        //    // WARNING !! Hard coded connection string is bad and cannot be pushed to repo
        //    optionsBuilder.UseSqlServer(@"BackTogetherDatabase");
        //}
    }
}
