using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Commands
{
    public class ToggleFavoriteTrainingProgramCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsFavourite { get; set; }
        public string UserId { get; set; }




        public class ToggleFavoriteTrainingProgramCommandHandler : IRequestHandler<ToggleFavoriteTrainingProgramCommand>
        {
            private readonly IApplicationDbContext _context;

            public ToggleFavoriteTrainingProgramCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ToggleFavoriteTrainingProgramCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var notes = new List<TrainingProgramNote>
{
    new TrainingProgramNote { TextAr = "كيفية التسجيل في البرنامج", TextEn = "How to register for the program", SortIndex = 1,TrainingProgramId = 2 ,},
    new TrainingProgramNote { TextAr = "تفاصيل المحاضرات والدورات التدريبية", TextEn = "Details of lectures and training courses", SortIndex = 2 ,TrainingProgramId = 2},
    new TrainingProgramNote { TextAr = "المواضيع التي سيتم تغطيتها في البرنامج", TextEn = "Topics to be covered in the program", SortIndex = 3 , TrainingProgramId = 2},
    new TrainingProgramNote { TextAr = "متطلبات الحضور والمشاركة في البرنامج", TextEn = "Requirements for attendance and participation in the program", SortIndex = 4 , TrainingProgramId = 2},
    new TrainingProgramNote { TextAr = "طرق الدفع المتاحة لرسوم البرنامج", TextEn = "Available payment methods for program fees", SortIndex = 5 ,TrainingProgramId = 2}
};

                    _context.TrainingProgramNotes.AddRange(notes);
                    await _context.SaveChangesAsync();



                    var program = await _context.TrainingPrograms.FindAsync(request.Id);
                    if (program == null)
                    {
                        throw new ArgumentException("Invalid training program ID");
                    }

                    var traineeId = await _context.Trainees
                                       .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                                       .Select(a => a.Id)
                                       .FirstOrDefaultAsync(cancellationToken);

                    var trainee = await _context.Trainees.FindAsync(traineeId);
                    if (trainee == null)
                    {
                        throw new ArgumentException("Invalid trainee ID");
                    }
                    var favouriteProgram = await _context.TraineeFavouritePrograms
                        .FirstOrDefaultAsync(f => f.TraineeId == traineeId && f.TrainingProgramId == request.Id);

                    if (request.IsFavourite && favouriteProgram == null)
                    {
                        _context.TraineeFavouritePrograms.Add(new TraineeFavouriteProgram
                        {
                            TraineeId = traineeId,
                            TrainingProgramId = request.Id
                        });
                    }
                    else if (!request.IsFavourite && favouriteProgram != null)
                    {
                        _context.TraineeFavouritePrograms.Remove(favouriteProgram);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

            }

          
        }
    }

}
