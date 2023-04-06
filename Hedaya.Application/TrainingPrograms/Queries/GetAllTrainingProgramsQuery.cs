using Hedaya.Application.Interfaces;
using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Domain.Entities;
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

                    var program1 = new TrainingProgram
                    {
                        TitleAr = "برنامج تدريبي 1",
                        TitleEn = "Training Program 1",
                        Description = "This is a training program description.",
                        StartDate = DateTime.UtcNow.AddDays(7),
                        EndDate = DateTime.UtcNow.AddDays(14),
                        ImgUrl = "https://example.com/image.jpg",
                        SubCategoryId = 1,
                        CreationDate = DateTime.UtcNow,
                        Deleted = false,
                        CreatedById = "1",
                    };
                    var program2 = new TrainingProgram
                    {
                        TitleAr = "برنامج تدريبي 2",
                        TitleEn = "Training Program 2",
                        Description = "This is another training program description.",
                        StartDate = DateTime.UtcNow.AddDays(14),
                        EndDate = DateTime.UtcNow.AddDays(21),
                        ImgUrl = "https://example.com/image2.jpg",
                        SubCategoryId = 2,
                        CreationDate = DateTime.UtcNow,
                        Deleted = false, CreatedById = "1",
                    };

                    _context.TrainingPrograms.AddRange(program1, program2);
                    _context.SaveChanges();



                    var PageSize = 10;
                    var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);

                    var trainingPrograms = await _context.TrainingPrograms
                        .Include(tp => tp.SubCategory)
                        .Include(tp => tp.TraineeFavouritePrograms)
                        .Select(tp => new TrainingProgramDto
                        {
                            SubCategoryName = tp.SubCategory.NameEn,
                            IsFav = _context.TraineeFavouritePrograms.Any(f => f.TrainingProgramId == tp.Id && f.TraineeId == traineeId),
                            ImgUrl = tp.ImgUrl,
                            Title = tp.TitleEn,
                            StartDate = tp.StartDate,

                        })
                        .Skip((request.PageNumber - 1) * PageSize)
                        .Take(PageSize)
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
