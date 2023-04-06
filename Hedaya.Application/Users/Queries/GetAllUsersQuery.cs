using Hedaya.Application.Users.Models;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UsersLiDto>>
    {
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetAllUsersQuery, List<UsersLiDto>>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<List<UsersLiDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var PageSize = 10;
                var users = _userManager.Users
                    .Select(user => new UsersLiDto
                    {
                        Id = user.Id,
                        userName = user.UserName,
                        Email = user.Email,
                        Roles = _userManager.GetRolesAsync(user).Result
                    })
                    .ToList();

                return users
                    .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
        }
    }
}
