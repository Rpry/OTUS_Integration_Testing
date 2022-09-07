using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// Контекст
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        /// <summary>
        /// Курсы
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        
        /// <summary>
        /// Уроки
        /// </summary>
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Course>()
                .HasMany(u => u.Lessons)
                .WithOne(c=> c.Course)
                .IsRequired();
        }
    }
}