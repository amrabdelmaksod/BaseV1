using Hedaya.Application.Friends.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Friends.Queries
{
    public class SearchForTraineesQuery : IRequest<object>
    {
        public string TraineeId { get; set; }
        public string SearchTerm { get; set; }
    }

    public class SearchForTraineesQueryHandler : IRequestHandler<SearchForTraineesQuery,object>
    {
        private readonly IApplicationDbContext _context;

        public SearchForTraineesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(SearchForTraineesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trainee = await _context.Trainees.FirstOrDefaultAsync(a => a.AppUserId == request.TraineeId);
                if (trainee == null)
                {
                  return  new { Message = $"There is no trainee with this id {request.TraineeId}" };
                }

                var friendIds = await _context.Friendships
                    .Where(f => f.Trainee.AppUserId == request.TraineeId && f.Status == FriendshipStatus.Accepted)
                    .Select(f => f.FriendId)
                    .ToListAsync(cancellationToken);

                var trainees = await _context.Trainees
                    .Where(t => t.AppUserId != request.TraineeId && (t.FullName.Contains(request.SearchTerm)))
                    .Select(t => new FriendDto
                    {
                        Id = t.Id,
                        Name = t.FullName,
                        IsFriend = friendIds.Contains(t.Id)
                    })
                    .ToListAsync(cancellationToken);

                return new { Result = trainees }  ;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }
    }

}
