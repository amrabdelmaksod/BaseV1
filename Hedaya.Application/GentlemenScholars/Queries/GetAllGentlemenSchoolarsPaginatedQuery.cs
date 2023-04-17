using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
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

         
              

               
                var totalCount = await Query.CountAsync(cancellationToken);
                // Calculate the total number of pages
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

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

               

                // Create a PaginatedList object to return the paginated Gentlemen Scholars and total count
                var pagedResults = new PaginatedList<GentlemenScholarDto>(
                    items: GentleMens,
                    totalCount: totalCount,
                    pageNumber: request.PageNumber,
                    pageSize: pageSize,
                    totalPages: totalPages);


                return new { Result = pagedResults }  ;
            }
        }



    }
}
