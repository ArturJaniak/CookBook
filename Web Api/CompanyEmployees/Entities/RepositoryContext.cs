using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Recipes)
                .WithOne(u => u.User)
                .HasForeignKey<Recipes>(u => u.UserId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Ratings)
                .WithOne(u => u.User)
                .HasForeignKey<Ratings>(u => u.UserId);
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Allergens> Allergens { get; set; }

        public DbSet<Tags> Tags { get; set; }

        public DbSet<Ratings> Ratings { get; set; }

        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Recipes> Recipes { get; set; }

        public DbSet<RecipeList> RecipeList { get; set; }
    }
}
