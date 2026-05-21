using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Projects.Src.Models;
namespace Projects.Src.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                optionsBuilder.UseSqlServer(builder.Build().GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<Instructor>()
                        .HasDiscriminator<string>("Discriminator")
                        .HasValue<FulltimeInstructor>("FullTime Instructor")
                        .HasValue<ParttimeInstructor>("PartTime Instructor");

            modelBuilder.Entity<Result>()
                        .Property(r => r.Name)
                        .HasColumnName("Description");

            modelBuilder.Entity<Student>().Property(s => s.Faculty).HasConversion<string>();
            modelBuilder.Entity<Student>().Property(s => s.Status).HasConversion<string>();
            modelBuilder.Entity<Student>().Property(s => s.Level).HasConversion<string>();

            modelBuilder.Entity<Instructor>().Property(i => i.Faculty).HasConversion<string>();

            modelBuilder.Entity<Course>().Property(c => c.Faculty).HasConversion<string>();

        }


    }
}
