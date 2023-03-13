using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Friends.Commands
{
    public class RespondToFriendRequestCommand : IRequest<Unit>
    {
        public string TraineeId { get; set; }
        public string FriendId { get; set; }
        public bool Accepted { get; set; }

        public class RespondToFriendRequestCommandHandler : IRequestHandler<RespondToFriendRequestCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public RespondToFriendRequestCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RespondToFriendRequestCommand request, CancellationToken cancellationToken)
            {
                // Retrieve the friendship request from the database
                var friendship = await _context.Friendships
                    .FirstOrDefaultAsync(f => f.Trainee.AppUserId == request.TraineeId && f.FriendId == request.FriendId);

                if (friendship == null)
                {
                    throw new NotFoundException(nameof(Friendship));
                }

                if (request.Accepted)
                {
                    // Accept the friend request
                    friendship.Status = FriendshipStatus.Accepted;
                    friendship.AcceptedDate = DateTime.UtcNow;
                }
                else
                {
                    // Reject the friend request
                    friendship.Status = FriendshipStatus.Rejected;
                }

                _context.Friendships.Update(friendship);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }

}
