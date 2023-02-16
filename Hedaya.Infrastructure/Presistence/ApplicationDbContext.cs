
using Hedaya.Application.Interfaces;
using Hedaya.Domain;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
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


        #region Entities
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<EducationalCourse> EducationalCourses { get; set; }
        public DbSet<CourseTopic> CourseTopics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<TestClass> TestClasses { get; set; }
        #endregion

        public override DatabaseFacade Database => base.Database;
        public override ChangeTracker ChangeTracker => base.ChangeTracker;

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
