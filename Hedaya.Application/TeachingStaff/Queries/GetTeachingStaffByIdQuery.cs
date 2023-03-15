using Hedaya.Application.Interfaces;
using Hedaya.Application.TeachingStaff.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.TeachingStaff.Queries
{
    public class GetTeachingStaffByIdQuery : IRequest<object>
    {
        public string Id { get; set; }

        public class GetTeachingStaffByIdQueryHandler : IRequestHandler<GetTeachingStaffByIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetTeachingStaffByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetTeachingStaffByIdQuery request, CancellationToken cancellationToken)
            {
                var teachingStaff = await _context.TeachingStaff
                    .Include(ts => ts.Tutorials)
                    .FirstOrDefaultAsync(ts => ts.Id == request.Id, cancellationToken);

                if (teachingStaff == null)
                {
                    return new { Message = $"Sorry, There is no TeachingStaff With This Id : {request.Id}" };
                }

                var teachingStaffDto = new TeachingStaffDto
                {
                    Id = teachingStaff.Id,
                    ImageURL = teachingStaff.ImageURL,
                    FullName = teachingStaff.FullName,
                    Description = teachingStaff.Description,
                    Facebook = teachingStaff.Facebook,
                    Twitter = teachingStaff.Twitter,
                    Youtube = teachingStaff.Youtube,
                    Tutorials = teachingStaff.Tutorials.Select(t => new TutorialDto
                    {
                        Id = t.Id,
                        Title = t.Title
                    }).ToList()
                };

                return new { Result = teachingStaffDto } ;
            }
        }
    }

}
