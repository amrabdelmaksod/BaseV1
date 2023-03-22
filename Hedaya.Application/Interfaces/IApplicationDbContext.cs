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
        DbSet<Course> Courses { get; set; }
        DbSet<CourseTopic> CourseTopics { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Blog> Blogs { get; set; }
        DbSet<TestClass> TestClasses { get; set; }
        DbSet<TestEntity> TestEntities { get; set; }
        DbSet<Complex> Complexes { get; set; }
        DbSet<GentlemenScholar> GentlemenScholars { get; set; }
        DbSet<PlatformField> PlatformFields { get; set; }
        DbSet<PlatformWorkAxes> PlatformWorkAxes { get; set; }
        DbSet<PlatformFeature> PlatformFeatures { get; set; }
        DbSet<Trainee> Trainees { get; set; }
        DbSet<Instructor> Instructors { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<CommonQuestion> CommonQuestions { get; set; }
        DbSet<SuggestionAndComplaint> SuggestionAndComplaints { get; set; }
        DbSet<Podcast> Podcasts { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Domain.Entities.TeachingStaff> TeachingStaff { get; set; }
        DbSet<Tutorial> Tutorials { get; set; }
        DbSet<MainCategory> MainCategories { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<MassCulture> MassCultures { get; set; }
        DbSet<ChatQuestions> ChatQuestions { get; set; }
        DbSet<MethodologicalExplanation> MethodologicalExplanations { get; set; }
        DbSet<ExplanationVideo> ExplanationVideos { get; set; }
       
        





        DatabaseFacade Database { get;  }

        ChangeTracker ChangeTracker { get;}
        void Dispose();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));






        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
