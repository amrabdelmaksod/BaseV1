using Hedaya.Application.Interfaces;
using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class FilterTrainingProgramsQuery : IRequest<object>
{
    public string UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int? SubCategoryId { get; set; }
    public bool SortByDurationAscending { get; set; }
    public string? SearchKeyword { get; set; }

    public class FilterTrainingProgramsQueryHandler : IRequestHandler<FilterTrainingProgramsQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public FilterTrainingProgramsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(FilterTrainingProgramsQuery request, CancellationToken cancellationToken)
        {

         

            var PageSize = 10;


            var traineeId = await _context.Trainees
                .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var query = _context.TrainingPrograms
                .Include(tp => tp.SubCategory)
                .Include(tp => tp.TraineeFavouritePrograms)
                .Where(tp => !tp.Deleted);

            if (request.SubCategoryId.HasValue)
            {
                query = query.Where(tp => tp.SubCategoryId == request.SubCategoryId.Value);
            }

            if (request.SortByDurationAscending)
            {
                query = query.OrderBy(tp => tp.Courses.Aggregate(TimeSpan.Zero, (acc, c) => acc + c.Duration));
            }
            else
            {
                query = query.OrderByDescending(tp => tp.Courses.Aggregate(TimeSpan.Zero, (acc, c) => acc + c.Duration));
            }


            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                query = query.Where(tp => tp.TitleEn.Contains(request.SearchKeyword) || tp.TitleAr.Contains(request.SearchKeyword));
            }

            var trainingProgramsCount = await query.CountAsync(cancellationToken);
            var trainingPrograms = await query
                .Skip((request.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(tp => new TrainingProgramDto
                {
                   
                    SubCategoryName = tp.SubCategory.NameEn,
                    IsFav = _context.TraineeFavouritePrograms.Any(f => f.TrainingProgramId == tp.Id && f.TraineeId == traineeId),
                    ImgUrl = tp.ImgUrl,
                    Title = tp.TitleEn,
                    StartDate = tp.StartDate,
                  
                })
                .ToListAsync(cancellationToken);

            return new {result = trainingPrograms, Count = trainingProgramsCount };
        }
    }
}
