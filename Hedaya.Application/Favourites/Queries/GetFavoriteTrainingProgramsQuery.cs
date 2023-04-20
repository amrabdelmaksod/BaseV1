using System.Globalization;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.TrainingPrograms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hedaya.Application.Favourites.Queries
{
    public class GetFavoriteTrainingProgramsQuery : IRequest<object>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetFavoriteTrainingProgramsQueryHandler : IRequestHandler<GetFavoriteTrainingProgramsQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetFavoriteTrainingProgramsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetFavoriteTrainingProgramsQuery request, CancellationToken cancellationToken)
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

            var trainingProgramsQuery = _context.TrainingPrograms
                .Where(p => p.TraineeFavouritePrograms.Any(f => f.TraineeId == traineeId))
                .Include(p => p.SubCategory)
                .OrderByDescending(p => p.Id);

            var totalCount = await trainingProgramsQuery.CountAsync(cancellationToken);

            var trainingPrograms = await trainingProgramsQuery
                .Skip((request.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);

            var trainingProgramDtos = trainingPrograms.Select(p => new TrainingProgramDto
            {
                Id = p.Id,
                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? p.TitleAr : p.TitleEn,
                StartDate = p.StartDate,
                ImgUrl = p.ImgUrl,
                IsFav = true,
                SubCategoryName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? p.SubCategory.NameAr : p.SubCategory.NameEn,
            }).ToList();

            var Favourites = new PaginatedList<TrainingProgramDto>(trainingProgramDtos, totalCount, request.PageNumber, PageSize, (int)Math.Ceiling((double)totalCount / PageSize));
            return new { Result = Favourites };
        }
    }
}
