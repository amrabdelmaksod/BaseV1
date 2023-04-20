using Hedaya.Application.Helpers;
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

                    var totalCount = await _context.TrainingPrograms.CountAsync(cancellationToken);
                    var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);


                    var trainingPrograms = await _context.TrainingPrograms
                        .Include(tp => tp.SubCategory)
                        .Include(tp => tp.TraineeFavouritePrograms)
                        .Select(tp => new TrainingProgramDto
                        {
                            Id = tp.Id,
                            SubCategoryName = tp.SubCategory.NameEn,
                            IsFav = _context.TraineeFavouritePrograms.Any(f => f.TrainingProgramId == tp.Id && f.TraineeId == traineeId),
                            ImgUrl = tp.ImgUrl,
                            Title = tp.TitleEn,
                            StartDate = tp.StartDate,

                        })
                        .Skip((request.PageNumber - 1) * PageSize)
                        .Take(PageSize)
                        .ToListAsync(cancellationToken);

                    var AllPrograms = new PaginatedList<TrainingProgramDto>(trainingPrograms, totalCount, request.PageNumber, PageSize, totalPages);

                    return new {result = AllPrograms};
                }
                catch (Exception ex) 
                {

                    throw new Exception(ex.Message);
                }
               
            }
        }
    }
}
