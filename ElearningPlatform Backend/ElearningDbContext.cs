using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Data
{
    public class ElearningDbContext : DbContext
    {
        public ElearningDbContext(DbContextOptions<ElearningDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.Property(e => e.CourseId).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                
                entity.HasOne(c => c.Creator)
                      .WithMany(u => u.CreatedCourses)
                      .HasForeignKey(c => c.CreatedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Lesson entity
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonId);
                entity.Property(e => e.LessonId).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                
                entity.HasOne(l => l.Course)
                      .WithMany(c => c.Lessons)
                      .HasForeignKey(l => l.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Quiz entity
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.QuizId);
                entity.Property(e => e.QuizId).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                
                entity.HasOne(q => q.Course)
                      .WithMany(c => c.Quizzes)
                      .HasForeignKey(q => q.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Question entity
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.QuestionId);
                entity.Property(e => e.QuestionId).ValueGeneratedOnAdd();
                entity.Property(e => e.QuestionText).IsRequired();
                entity.Property(e => e.OptionA).IsRequired().HasMaxLength(255);
                entity.Property(e => e.OptionB).IsRequired().HasMaxLength(255);
                entity.Property(e => e.OptionC).IsRequired().HasMaxLength(255);
                entity.Property(e => e.OptionD).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CorrectAnswer).IsRequired().HasMaxLength(1);
                
                entity.HasOne(q => q.Quiz)
                      .WithMany(qz => qz.Questions)
                      .HasForeignKey(q => q.QuizId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Result entity
            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => e.ResultId);
                entity.Property(e => e.ResultId).ValueGeneratedOnAdd();
                entity.Property(e => e.AttemptDate).HasDefaultValueSql("GETDATE()");
                
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Results)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                      
                entity.HasOne(r => r.Quiz)
                      .WithMany(q => q.Results)
                      .HasForeignKey(r => r.QuizId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
