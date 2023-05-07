using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Favourites.Queries
{
    public class GetFavoriteExplanationsQuery : IRequest<object>
    {
        public int PageNumber { get; set; } = 1;
        public string UserId { get; set; }

        public class GetFavoriteExplanationsQueryHandler : IRequestHandler<GetFavoriteExplanationsQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetFavoriteExplanationsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetFavoriteExplanationsQuery request, CancellationToken cancellationToken)
            {
                var PageSize = 10;
                var traineeId = await _context.Trainees
                       .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                       .Select(a => a.Id)
                       .FirstOrDefaultAsync(cancellationToken);

                var trainee = await _context.Trainees.FindAsync(traineeId);
                if (trainee == null)
                {
                    throw new ArgumentException("Invalid trainee ID");
                }

                var explanationsQuery = _context.MethodologicalExplanations
                    .Where(e => e.TraineeExplanationFavourites.Any(f => f.TraineeId == traineeId))
                    .OrderByDescending(e => e.Id);

                var totalCount = await explanationsQuery.CountAsync(cancellationToken);

                var explanations = await explanationsQuery
                .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(cancellationToken);

                var explanationDtos = explanations.Select(e => new MethodlogicalExplanationDto
                {
                    Id = e.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? e.TitleAr : e.TitleEn,
                    Description = e.Description,
                    IsFav = true,
                    SubCategoryId = e.SubCategoryId,
                    ImageUrl = e.ImageUrl,
                    Duration = e.Duration.ToString()   
                }).ToList();

                var Favourites = new PaginatedList<MethodlogicalExplanationDto>(explanationDtos, totalCount, request.PageNumber, PageSize, (int)Math.Ceiling((double)totalCount / PageSize));
                return new { Result = Favourites };
            }
        }

    }

}
