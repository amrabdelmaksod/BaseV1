using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public UserType UserType { get; set; }
        public Nationality Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string RoleId { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
               var SecurityCode = new Random().Next(100000, 999999).ToString();

                var user = new AppUser
                {
                    FullName = request.FullName,
                    UserName = request.Phone,
                    Email = request.Email,
                    UserType = request.UserType,
                    Nationality = request.Nationality,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    SecurityCode = SecurityCode
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    // Add the user to the specified role
                    var role = await _roleManager.FindByIdAsync(request.RoleId);
                    if (role == null)
                    {
                        throw new ApplicationException($"Role with ID {request.RoleId} not found.");
                    }
                    await _userManager.AddToRoleAsync(user, role.Name);

                    return user.Id;
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new ApplicationException($"Unable to create user: {errors}");
                }
            }
        }

    }

}
