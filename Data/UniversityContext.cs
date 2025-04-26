using Microsoft.EntityFrameworkCore;
using university_management.Models;

namespace university_management.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UTC for all DateTime properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        modelBuilder.Entity(entityType.Name)
                            .Property<DateTime>(property.Name)
                            .HasConversion(
                                v => v.Kind == DateTimeKind.Unspecified 
                                    ? DateTime.SpecifyKind(v, DateTimeKind.Utc) 
                                    : v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        modelBuilder.Entity(entityType.Name)
                            .Property<DateTime?>(property.Name)
                            .HasConversion(
                                v => v.HasValue 
                                    ? (v.Value.Kind == DateTimeKind.Unspecified 
                                        ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) 
                                        : v.Value.ToUniversalTime())
                                    : v,
                                v => v.HasValue 
                                    ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) 
                                    : v);
                    }
                }
            }

            // Configure relationships
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}