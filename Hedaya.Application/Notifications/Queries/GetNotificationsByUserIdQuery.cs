using Hedaya.Application.Interfaces;
using Hedaya.Application.Notifications.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Notifications.Queries
{
    public class GetNotificationsByUserIdQuery : IRequest<object>
    {
        public string UserId { get; }

        public GetNotificationsByUserIdQuery(string userId)
        {
            UserId = userId;
        }


        public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetNotificationsByUserIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
            {              
                var notifications = await _context.Notifications.Where(n => n.AppUserId == request.UserId)
                    .Select(a => new NotificationLiDto { Content = a.Content, Date = a.Date, Title = a.Title, UrlLink = a.UrlLink })
                    .ToListAsync();

                return new { Result = notifications } ;
            }
        }

    }

}
