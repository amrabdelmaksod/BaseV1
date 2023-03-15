using Hedaya.Application.Friends.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetPendingFriendRequestsQuery : IRequest<object>
{
    public string TraineeId { get; set; }
}

public class GetPendingFriendRequestsQueryHandler : IRequestHandler<GetPendingFriendRequestsQuery, object>
{
    private readonly IApplicationDbContext _context;

    public GetPendingFriendRequestsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(GetPendingFriendRequestsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the pending friend requests for the trainee with the given ID
        var pendingFriendships = await _context.Friendships
            .Include(f => f.Friend)
            .Where(f => f.Trainee.AppUserId == request.TraineeId && f.Status == FriendshipStatus.Pending)
            .ToListAsync(cancellationToken);

        // Map the pending friendships to DTOs
        var friendRequestDtos = new List<FriendRequestDto>();

        foreach (var friendship in pendingFriendships)
        {
            friendRequestDtos.Add(new FriendRequestDto
            {
                Id = friendship.FriendId,
                TraineeId = friendship.TraineeId,
                FriendId = friendship.FriendId,
                RequestDate = friendship.RequestDate,
                AcceptedDate = friendship.AcceptedDate,
                FriendName = friendship.Friend.FullName // Assuming the Friend entity has a Name property
            });
        }

        return new { Result = friendRequestDtos}  ;
    }
}
