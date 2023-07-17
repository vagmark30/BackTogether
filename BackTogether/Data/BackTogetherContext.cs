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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
                .Entity<Project>()
                .HasOne(e => e.User)
                .WithMany(e => e.Projects)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<Project>()
                .HasMany(e => e.Backings)
                .WithOne(e => e.Project)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Backing>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Backings)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<Backing>()
                .HasOne(e => e.User)
                .WithMany(e => e.Backings)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<User>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.NoAction);

            // Data Seeding
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "aFEf4w4f", Password = "NZ#7eYB%", Email = "example@email.com", HasAdminPrivileges = true },
                new User { Id = 2, Username = "fa4gfwff", Password = "6*%7rKNd", Email = "example1@email.com", HasAdminPrivileges = true },
                new User { Id = 3, Username = "tejh56eu", Password = "K^aB%s6T", Email = "example2@email.com", HasAdminPrivileges = false },
                new User { Id = 4, Username = "f34g34qg", Password = "Fg75^U@j", Email = "example3@email.com", HasAdminPrivileges = false },
                new User { Id = 5, Username = "fq34gqgf", Password = "#VEGu3it", Email = "example4@email.com", HasAdminPrivileges = false },
                new User { Id = 6, Username = "qf34gq3g", Password = "Cnk@XH23", Email = "example5@email.com", HasAdminPrivileges = false },
                new User { Id = 7, Username = "f34qg4q3", Password = "HpKY6N%X", Email = "example6@email.com", HasAdminPrivileges = true },
                new User { Id = 8, Username = "n4eh6wqw", Password = "P6@%R6%a", Email = "example7@email.com", HasAdminPrivileges = false }
            );
        }

        // WARNING! Could break things, keep only if it doesnt break the DB
        // Make tables have singular names instead of plural
        // !!! Probably causes a name conflict so we remove it for now !!!
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        //    configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
        //}

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
