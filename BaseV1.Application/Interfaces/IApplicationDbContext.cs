using BaseV1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace BaseV1.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TestClass> Tests { get; set; }



        DatabaseFacade Database { get;  }

        ChangeTracker ChangeTracker { get;}
        void Dispose();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
