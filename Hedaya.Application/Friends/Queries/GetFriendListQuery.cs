using Hedaya.Application.Friends.Models;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hedaya.Application.Friends.Queries
{
    public class GetFriendListQuery : IRequest<object>
    {
        public string token { get; set; }
    }

    public class GetFriendListQueryHandler : IRequestHandler<GetFriendListQuery, object>
    {
        private readonly IApplicationDbContext _context;


        public GetFriendListQueryHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
       
        }

        public async Task<object> Handle(GetFriendListQuery request, CancellationToken cancellationToken)
        {

            string userIdFromToken = JWTHelper.GetUserIdFromToken(request.token);
            var user = await _context.AppUsers.FirstOrDefaultAsync(a=>a.Id == userIdFromToken);
            if (user == null)
            {
               
                return new  { Message = $"Not Found User With Id {userIdFromToken}", Result = new {}};
            }

            var trainee = await _context.Trainees.FirstOrDefaultAsync(a=>a.AppUserId==userIdFromToken);
            if (trainee == null)
            {
                return new  { Message = $"Not Found Trainee With Id {userIdFromToken}"};
            }

            var friendIds = await _context.Friendships.Include(a=>a.Trainee).ThenInclude(a=>a.AppUser)
                .Where(f => f.Trainee.AppUserId == userIdFromToken&&f.Status == FriendshipStatus.Accepted)
                .Select(f => f.FriendId)
                .ToListAsync(cancellationToken);

            var friends = await _context.Trainees
                .Where(t => friendIds.Contains(t.Id))
                .Select(t => new FriendDto
                {
                    Id = t.Id,
                    Name = t.FullName,
                    IsFriend = true
                })
                .ToListAsync(cancellationToken);

            return new  {Result =  friends };
        }
    }

}
