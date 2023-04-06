namespace Hedaya.Application.Users.Commands.DeleteUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Hedaya.Domain.Entities.Authintication;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class DeleteUserCommand : IRequest
    {
        public string Id { get; set; }
        public string DeletedReason { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new ApplicationException($"User with ID {request.Id} not found.");
            }
            user.Deleted = true;
            user.DeletedReason = request.DeletedReason;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Unable to delete user: {errors}");
            }

            return Unit.Value;
        }
    }
}
