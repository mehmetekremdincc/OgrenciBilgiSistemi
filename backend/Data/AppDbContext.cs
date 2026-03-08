using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Models;
using System.Linq.Expressions;

namespace OgrenciBilgiSistemiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseOffering> CourseOfferings => Set<CourseOffering>();
        public DbSet<StudentCourseOffering> StudentCourseOfferings => Set<StudentCourseOffering>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Schedule> Schedules => Set<Schedule>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(CreateIsActiveFilter(entityType.ClrType));
                }
            }

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(s => s.StudentNumber).IsUnique();
                entity.HasOne(s => s.User)
                      .WithOne(u => u.Student)
                      .HasForeignKey<Student>(s => s.UserId);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasOne(t => t.User)
                      .WithOne(u => u.Teacher)
                      .HasForeignKey<Teacher>(t => t.UserId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(c => c.Code).IsUnique();
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(g => g.Midterm).HasColumnType("decimal(5,2)");
                entity.Property(g => g.Final).HasColumnType("decimal(5,2)");
                entity.Property(g => g.Average).HasColumnType("decimal(5,2)");
                entity.Property(g => g.LetterGrade).HasMaxLength(2);
            });

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.CourseOffering)
                .WithMany(co => co.Schedules)
                .HasForeignKey(s => s.CourseOfferingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.StudentCourseOffering)
                .WithMany(sco => sco.Attendances)
                .HasForeignKey(a => a.StudentCourseOfferingId);
        }

        private static LambdaExpression CreateIsActiveFilter(Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var prop = Expression.Property(param, nameof(BaseEntity.IsActive));
            var body = Expression.Equal(prop, Expression.Constant(true));
            var delegateType = typeof(Func<,>).MakeGenericType(entityType, typeof(bool));
            return Expression.Lambda(delegateType, body, param);
        }
    }
}