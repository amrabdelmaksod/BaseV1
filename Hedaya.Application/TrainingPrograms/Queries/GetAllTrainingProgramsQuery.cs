using Hedaya.Application.Interfaces;
using Hedaya.Application.TrainingPrograms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.TrainingPrograms.Queries
{
    public class GetAllTrainingProgramsQuery : IRequest<object>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
     

        public class GetAllTrainingProgramsQueryHandler : IRequestHandler<GetAllTrainingProgramsQuery,object>
        {
            private readonly IApplicationDbContext _context;

            public GetAllTrainingProgramsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAllTrainingProgramsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var PageSize = 10;
                    var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);

                    var trainingPrograms = await _context.TrainingPrograms
                        .Include(tp => tp.SubCategory)
                        .Include(tp => tp.TraineeFavouritePrograms)
                         .Skip((request.PageNumber - 1) * PageSize)
                        .Take(PageSize)
                        .Select(tp => new TrainingProgramDto
                        {
                            SubCategoryName = tp.SubCategory.NameEn,
                            IsFav = _context.TraineeFavouritePrograms.Any(f => f.TrainingProgramId == tp.Id && f.TraineeId == traineeId),
                            ImgUrl = tp.ImgUrl,
                            Title = tp.TitleEn,
                            StartDate = tp.StartDate
                        })
                       
                        .ToListAsync(cancellationToken);

                    return new { result = trainingPrograms };
                }
                catch (Exception ex) 
                {

                    throw new Exception(ex.Message);
                }
               
            }
        }
    }
}
