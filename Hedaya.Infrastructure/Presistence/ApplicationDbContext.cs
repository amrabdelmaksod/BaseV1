
using Hedaya.Application.Interfaces;
using Hedaya.Domain;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
using Hedaya.Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hedaya.Infrastructure.Presistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        #region Users
        public DbSet<AppUser> AppUsers { get; set; }
        #endregion


        #region Hedaya Entities
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTopic> CourseTopics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<TestClass> TestClasses { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Complex> Complexes { get; set; }
        public DbSet<GentlemenScholar> GentlemenScholars { get; set; }

        public DbSet<PlatformField> PlatformFields { get ; set; }
        public DbSet<PlatformWorkAxes> PlatformWorkAxes { get; set; }
        public DbSet<PlatformFeature> PlatformFeatures { get ; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<CommonQuestion> CommonQuestions { get; set; }
        public DbSet<SuggestionAndComplaint> SuggestionAndComplaints { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Domain.Entities.TeachingStaff> TeachingStaff { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<MainCategory> MainCategories { get; set ; }
        public DbSet<SubCategory> SubCategories { get ; set; }
        public DbSet<MassCulture> MassCultures { get; set; }
        public DbSet<Domain.Entities.ChatQuestions> ChatQuestions { get; set; }
        public DbSet<MethodologicalExplanation> MethodologicalExplanations { get; set; }
        public DbSet<ExplanationVideo> ExplanationVideos { get; set; }
        public DbSet<TraineeCourseFavorite> TraineeCourseFavorites { get; set; }
        public DbSet<TraineeLesson> TraineeLessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TraineeAnswer> TraineeAnswers { get; set; }
        public DbSet<CourseTest> CourseTests { get; set; }


        #endregion




        public override DatabaseFacade Database => base.Database;
        public override ChangeTracker ChangeTracker => base.ChangeTracker;


        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
