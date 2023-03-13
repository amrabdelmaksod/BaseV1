using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Friends.Commands
{
    public class SendFriendRequestCommand : IRequest<Unit>
    {
        public string TraineeId { get; set; }
        public string FriendId { get; set; }

        public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public SendFriendRequestCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Retrieve the trainees involved in the friend request
                    var trainee = await _context.Trainees.FirstOrDefaultAsync(a => a.AppUserId == request.TraineeId);
                    var friend = await _context.Trainees.FirstOrDefaultAsync(a => a.Id == request.FriendId);

                    // Check if the friend request already exists
                    var existingFriendship = await _context.Friendships
                        .FirstOrDefaultAsync(f => f.TraineeId == request.TraineeId && f.FriendId == request.FriendId);

                    if (existingFriendship != null)
                    {
                        // Update the existing friendship with the new request
                        existingFriendship.Status = FriendshipStatus.Pending;
                        _context.Friendships.Update(existingFriendship);
                    }
                    else
                    {
                        // Create a new friendship request
                        var friendship = new Friendship
                        {
                            TraineeId = trainee.Id,
                            FriendId = friend.Id,
                            RequestDate = DateTime.UtcNow,
                            Status = FriendshipStatus.Pending
                        };

                        _context.Friendships.Add(friendship);
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
