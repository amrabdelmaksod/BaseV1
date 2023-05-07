using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.MethodologicalExplanations.Queries
{
    public class GetAllMethodologicalExplanationsByInstructorIdQuery : IRequest<object>
    {
        public string InstructorId { get; set; }
        public int PageNumber { get; set; }
        public string UserId { get; set; }


        public class GetMethodologicalExplanationsByInstructorIdQueryHandler : IRequestHandler<GetAllMethodologicalExplanationsByInstructorIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetMethodologicalExplanationsByInstructorIdQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<object> Handle(GetAllMethodologicalExplanationsByInstructorIdQuery query, CancellationToken cancellationToken)
            {
                var PageSize = 10;

                var traineeId = await _context.Trainees
                .Where(a => a.AppUserId == query.UserId && !a.Deleted)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);

                var totalCount = await _context.MethodologicalExplanations.CountAsync(x => x.InstructorId == query.InstructorId);

                var methodologicalExplanations = await _context.MethodologicalExplanations
                                            .Where(x => x.InstructorId == query.InstructorId)
                                            .OrderByDescending(x => x.Id)
                                            .Skip((query.PageNumber - 1) * PageSize)
                                            .Take(PageSize)
                                            .Select(x => new MethodlogicalExplanationDto
                                            {
                                                Id = x.Id,
                                                SubCategoryId = x.SubCategoryId,
                                                Title = x.TitleEn,
                                                Description = x.Description,
                                                IsFav = _context.TraineeExplanationFavourite.Any(f => f.MethodologicalExplanationId == x.Id && f.TraineeId == traineeId),
                                                Duration = x.Duration.ToString(),
                                                ImageUrl = x.ImageUrl
                                            })
                                            .ToListAsync();

                var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                var result = new PaginatedList<MethodlogicalExplanationDto>(methodologicalExplanations, totalCount, query.PageNumber, PageSize, totalPages);

                return new {Result = result };
            }
        }


    }

}
