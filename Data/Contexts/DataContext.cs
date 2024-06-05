

using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<UserCoursesEntity> UserCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseEntity>().ToContainer("Courses");
        modelBuilder.Entity<CourseEntity>().HasPartitionKey(c => c.Id);
        modelBuilder.Entity<CourseEntity>().OwnsOne(c => c.Prices);
        modelBuilder.Entity<CourseEntity>().OwnsMany(c => c.Authors);
        modelBuilder.Entity<CourseEntity>().OwnsOne(c => c.Content, content => { content.OwnsMany(c => c.ProgramDetails); });
        modelBuilder.Entity<UserCoursesEntity>().ToContainer("UserCourses");
        modelBuilder.Entity<UserCoursesEntity>().HasPartitionKey(c => c.UserId);
    }
}
