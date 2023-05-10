using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Commands
{
    public class ToggleFavoriteTrainingProgramCommand : IRequest<object>
    {
        public int Id { get; set; }
        public bool IsFavourite { get; set; }
        public string UserId { get; set; }




        public class ToggleFavoriteTrainingProgramCommandHandler : IRequestHandler<ToggleFavoriteTrainingProgramCommand,object>
        {
            private readonly IApplicationDbContext _context;

            public ToggleFavoriteTrainingProgramCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(ToggleFavoriteTrainingProgramCommand request, CancellationToken cancellationToken)
            {
                try
                {
                   

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

                    return new { ItemId = program.Id };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

            }

          
        }
    }

}
