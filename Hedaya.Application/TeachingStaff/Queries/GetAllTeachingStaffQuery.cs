using Hedaya.Application.Interfaces;
using Hedaya.Application.TeachingStaff.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.TeachingStaff.Queries
{
    public class GetAllTeachingStaffQuery : IRequest<object>
    {
        public int PageNumber { get; set; }


        public class GetAllTeachingStaffQueryHandler : IRequestHandler<GetAllTeachingStaffQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetAllTeachingStaffQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAllTeachingStaffQuery request, CancellationToken cancellationToken)
            {

                const int pageSize = 10; // Define the number of items to be shown on each page
                var skip = (request.PageNumber - 1) * pageSize;

                var teachingStaffList = await _context.TeachingStaff
                    .Include(ts => ts.Tutorials)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                var teachingStaffDtoList = teachingStaffList.Select(ts => new TeachingStaffDto
                {
                    Id = ts.Id,
                    ImageURL = ts.ImageURL,
                    FullName = ts.FullName,
                    Description = ts.Description,
                    Facebook = ts.Facebook,
                    Twitter = ts.Twitter,
                    Youtube = ts.Youtube,
                    Tutorials = ts.Tutorials.Select(t => new TutorialDto
                    {
                        Id = t.Id,
                        Title = t.Title
                    }).ToList()
                }).ToList();

                return new { Result = teachingStaffDtoList };
            }

        }
    }

}
