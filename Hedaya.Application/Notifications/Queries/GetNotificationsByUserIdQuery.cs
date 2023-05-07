using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.Notifications.Models;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hedaya.Application.Notifications.Queries
{
    public class GetNotificationsByUserIdQuery : IRequest<object>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }

        public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetNotificationsByUserIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var pageSize = 10;

                var notifications = await _context.Notifications
                    .Where(n => n.AppUserId == request.UserId)
                    .OrderByDescending(n => n.Date)
                    .Skip((request.PageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(n => new NotificationLiDto
                    {
                        Content = n.Content,
                        Date = n.Date,
                        Title = n.Title,
                        Type = NotificationType.Message
                    })
                    .ToListAsync(cancellationToken);

                var totalCount = await _context.Notifications
                    .Where(n => n.AppUserId == request.UserId)
                    .CountAsync(cancellationToken);

                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var paginatedList = new PaginatedList<object>(notifications, totalCount, request.PageNumber, pageSize, totalPages);

                return new { result = paginatedList };
            }
        }
    }
}
