using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hedaya.Application.Infrastructure
{
    public class GetPagedResultsQuery<TEntity> : IRequest<PagedResults<TEntity>> where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }

        public class GetPagedResultsQueryHandler<TEntity> : IRequestHandler<GetPagedResultsQuery<TEntity>, PagedResults<TEntity>> where TEntity : class
        {
            private readonly IApplicationDbContext _context;

            public GetPagedResultsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PagedResults<TEntity>> Handle(GetPagedResultsQuery<TEntity> request, CancellationToken cancellationToken)
            {
                var query = _context.Set<TEntity>().AsQueryable();

                // Apply filter expression, if specified
                if (request.Filter != null)
                {
                    query = query.Where(request.Filter);
                }

                var totalCount = await query.CountAsync(cancellationToken);

                // Apply ordering, if specified
                if (request.OrderBy != null)
                {
                    query = request.OrderBy(query);
                }

                // Apply paging
                query = query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize);

                var items = await query.ToListAsync(cancellationToken);

                return new PagedResults<TEntity>
                {
                    TotalCount = totalCount,
                    Items = items
                };
            }
        }

    }
}
