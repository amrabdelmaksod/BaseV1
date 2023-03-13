using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.GentlemenScholars.Queries
{
    public class GetAllGentlemenSchoolarsPaginatedQuery : IRequest<PagedResults<GentlemenScholarDto>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }

        public class GetAllGentlemenSchoolarsQueryHandler : IRequestHandler<GetAllGentlemenSchoolarsPaginatedQuery, PagedResults<GentlemenScholarDto>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllGentlemenSchoolarsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PagedResults<GentlemenScholarDto>> Handle(GetAllGentlemenSchoolarsPaginatedQuery request, CancellationToken cancellationToken)
            {
              


                var Query = _context.GentlemenScholars.AsQueryable();

                // Filter the courses by the search term, if provided
                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    Query = Query.Where(c => c.Title.Contains(request.SearchTerm));
                }

                // Order the courses by the specified property and direction
                switch (request.OrderBy.ToLower())
                {
                    case "name":
                        Query = request.OrderDirection.ToLower() == "asc"
                            ? Query.OrderBy(c => c.Name)
                            : Query.OrderByDescending(c => c.Name);
                        break;
                    case "title":
                        Query = request.OrderDirection.ToLower() == "asc"
                            ? Query.OrderBy(c => c.Title)
                            : Query.OrderByDescending(c => c.Title);
                        break;
                    default:
                        Query = Query.OrderByDescending(c => c.Name);
                        break;
                }

                // Get the total count of courses that match the search criteria
                var totalCount = await Query.CountAsync(cancellationToken);

                // Paginate the courses
                var GentleMens = await Query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                  .Select(a => new GentlemenScholarDto
                  {
                      Id = a.Id,
                      Name = a.Name,
                      Description = a.Description,
                      Facebook = a.Facebook,
                      Title = a.Title,
                      Twitter = a.Twitter,
                      Youtube = a.Youtube
                  }
                  )
                    .ToListAsync(cancellationToken);

                // Create a PagedResults object to return the paginated courses and total count
                var pagedResults = new PagedResults<GentlemenScholarDto>
                {
                    TotalCount = totalCount,
                    Items = GentleMens
                };

                return pagedResults;
            }
        }



    }
}
