using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Commands
{
    public class ToggleMethodologicalExplanationFavouriteCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsFavourite { get; set; }
        public string UserId { get; set; }

        public class ToggleMethodologicalExplanationFavouriteCommandHandler : IRequestHandler<ToggleMethodologicalExplanationFavouriteCommand>
        {
            private readonly IApplicationDbContext _context;

            public ToggleMethodologicalExplanationFavouriteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ToggleMethodologicalExplanationFavouriteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var methodologicalExplanation = await _context.MethodologicalExplanations.FindAsync(request.Id);
                    if (methodologicalExplanation == null)
                    {
                        throw new ArgumentException("Invalid methodological explanation ID");
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

                    var favouriteMethodologicalExplanation = await _context.TraineeExplanationFavourite
                        .FirstOrDefaultAsync(f => f.TraineeId == traineeId && f.MethodologicalExplanationId == request.Id);

                    if (request.IsFavourite && favouriteMethodologicalExplanation == null)
                    {
                        _context.TraineeExplanationFavourite.Add(new TraineeExplanationFavourite
                        {
                            TraineeId = traineeId,
                            MethodologicalExplanationId = request.Id
                        });
                    }
                    else if (!request.IsFavourite && favouriteMethodologicalExplanation != null)
                    {
                        _context.TraineeExplanationFavourite.Remove(favouriteMethodologicalExplanation);
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

