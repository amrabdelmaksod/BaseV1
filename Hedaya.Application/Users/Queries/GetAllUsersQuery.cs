using Hedaya.Application.Interfaces;
using Hedaya.Application.Users.Models;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UsersLiDto>>
    {
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetAllUsersQuery, List<UsersLiDto>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IApplicationDbContext _context;

            public Handler(UserManager<AppUser> userManager, IApplicationDbContext context)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<List<UsersLiDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var PageSize = 10;
                var users = _userManager.Users
                    .Select(user => new UsersLiDto
                    {
                        Id = user.Id,
                        Phone = user.UserName,
                        Email = user.Email,
                        Roles = _userManager.GetRolesAsync(user).Result,
                        UserType = user.UserType,
                        IsActive = user.IsActive,
                    })
                    .ToList();

                foreach (var user in users)
                {
                    var TraineeName = "";
                    if (user.UserType == Domain.Enums.UserType.User)
                    {
                         TraineeName = (await _context.Trainees.Where(a => a.AppUserId == user.Id).Select(a => a.FullName).FirstOrDefaultAsync(cancellationToken));
                    }
                 
                 
                    user.FullName = TraineeName??"";
                }

                return users
                    .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
        }
    }
}
