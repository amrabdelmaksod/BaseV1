using Hedaya.Application.Interfaces;
using Hedaya.Application.Notifications.Models;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Notifications.Queries
{
    public class GetNotificationsByUserIdQuery : IRequest<object>
    {
        public string UserId { get; }
        public int PageNumber { get; }
      

        public GetNotificationsByUserIdQuery(string userId, int pageNumber)
        {
            UserId = userId;
            PageNumber = pageNumber;
           
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

                var PageSize = 10;

                var notifications = await _context.Notifications
                    .Where(n => n.AppUserId == request.UserId)
                    .OrderByDescending(n => n.Date)
                    .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .Select(n => new NotificationLiDto { Content = n.Content, Date = n.Date, Title = n.Title, Type = NotificationType.Message })
                    .ToListAsync();

                var totalCount = await _context.Notifications
                    .Where(n => n.AppUserId == request.UserId)
                    .CountAsync();

                var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

                var data = new
                {
                    result = notifications,
                    PageNumber = request.PageNumber,
                    PageSize = PageSize,
                    TotalPages = totalPages,
                    TotalCount = totalCount
                };

                return new
                {
                  result = data
                };
            }
        }
    }


}
