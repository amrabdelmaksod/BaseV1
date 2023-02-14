using Hedaya.Domain;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Hedaya.Application.Interfaces
{
    public interface IApplicationDbContext
    {
    
        DbSet<Reply> Replies { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Forum> Forums { get; set; }
        DbSet<EducationalCourse> EducationalCourses { get; set; }
        DbSet<CourseTopic> CourseTopics { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<TestClass> TestClasses { get; set; }
        DbSet<TestEntity> TestEntities { get; set; }




        DatabaseFacade Database { get;  }

        ChangeTracker ChangeTracker { get;}
        void Dispose();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
