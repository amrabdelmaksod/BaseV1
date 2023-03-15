using Hedaya.Application.Users.Models;
using Hedaya.Domain.Common;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<object>
    {
        public UserParams userParams { get; set; } 
        public class Handler : IRequestHandler<GetAllUsersQuery, object>
        {
         
            private readonly UserManager<AppUser> _userManager;
            public Handler( UserManager<AppUser> userManager)
            {
              
                _userManager = userManager;
            }

            public async Task<object> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = _userManager.Users
            
                        .Select(user => new UsersLiDto { Id = user.Id, userName = user.UserName, Email = user.Email, Roles = _userManager.GetRolesAsync(user).Result })
            
                        .ToList();

            
                 

                    return new { Result = users } ;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
