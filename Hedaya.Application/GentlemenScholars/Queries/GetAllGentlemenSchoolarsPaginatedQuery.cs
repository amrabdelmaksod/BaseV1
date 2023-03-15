using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using iTextSharp.text;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.GentlemenScholars.Queries
{
    public class GetAllGentlemenSchoolarsPaginatedQuery : IRequest<object>
    {

        public int PageNumber { get; set; }
     

      

        public class GetAllGentlemenSchoolarsQueryHandler : IRequestHandler<GetAllGentlemenSchoolarsPaginatedQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetAllGentlemenSchoolarsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAllGentlemenSchoolarsPaginatedQuery request, CancellationToken cancellationToken)
            {
                
                var pageSize = 10;
                var Query = _context.GentlemenScholars.AsQueryable();

         
              

                // Get the total count of courses that match the search criteria
                var totalCount = await Query.CountAsync(cancellationToken);

                // Paginate the courses
                var GentleMens = await Query
                    .Skip((request.PageNumber - 1) * pageSize)
                    .Take(pageSize)
                  .Select(a => new GentlemenScholarDto
                  {
                      Id = a.Id,
                      Name = a.Name,
                      Description = a.Description,
                      Facebook = a.Facebook,
                      Title = a.Title,
                      Twitter = a.Twitter,
                      Youtube = a.Youtube,
                      ImageUrl = a.ImageUrl,
                  }
                  )
                    .ToListAsync(cancellationToken);

                // Create a PagedResults object to return the paginated courses and total count
                //To Do Paged Result with Total Counts
                var pagedResults = new PagedResults<GentlemenScholarDto>
                {
                    TotalCount = totalCount,
                    Items = GentleMens
                };

                return new { Result = GentleMens }  ;
            }
        }



    }
}
