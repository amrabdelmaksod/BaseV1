using Hedaya.Application.Interfaces;
using Hedaya.Application.Users.Models;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<object>
    {
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetAllUsersQuery, object>
        {
            private readonly UserManager<AppUser> _userManager;

            private readonly IApplicationDbContext _context;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<AppUser> userManager, IApplicationDbContext context, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _context = context;
                _roleManager = roleManager;
            }

            public async Task<object> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var PageSize = 10;
                    var totalCount = await _userManager.Users.CountAsync();
                    var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
                    var users = _userManager.Users.Skip((request.PageNumber - 1) * PageSize)
                        .Take(PageSize)

                        .Select(user => new UsersLiDto
                        {
                            Id = user.Id,
                            Phone = user.UserName,
                            Email = user.Email,
                            UserType = user.UserType,
                            IsActive = user.IsActive,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            Nationality = user.Nationality,
                            Roles = _userManager.GetRolesAsync(user).Result,                            
                            FullName = user.FullName,
                })
                        .ToList();

                    foreach (var user in users)
                    {
                        List<string> userRoleIds = new List<string>();
                        foreach (var roleName in user.Roles)
                        {
                            var roleId = _roleManager.FindByNameAsync(roleName).Result.Id;
                            userRoleIds.Add(roleId);
                        }
                        user.RolesIds = userRoleIds;


                        var TraineeName = "";
                        if (user.UserType == Domain.Enums.UserType.User&&string.IsNullOrEmpty( user.FullName))
                        {
                            TraineeName = (await _context.Trainees.Where(a => a.AppUserId == user.Id).Select(a => a.FullName).FirstOrDefaultAsync(cancellationToken));
                            user.FullName = TraineeName??"";

                        }


                    }

                    return new { Users = users, TotalCount = totalCount, TotalPages = totalPages };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
              
                   
            }
        }
    }
}
