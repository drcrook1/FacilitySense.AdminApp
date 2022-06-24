using Microsoft.EntityFrameworkCore;
using FacilitySense.DataModels;

namespace FacilitySense.Repositories.SQL
{
    public class FacilitySenseDBContext : DbContext
    {
        public FacilitySenseDBContext(DbContextOptions<FacilitySenseDBContext> options)
            : base(options)
        {
        }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().ToTable("Facility");
                //.Ignore(facility => facility.Latitude)
                //.Ignore(facility => facility.Longitude);
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
