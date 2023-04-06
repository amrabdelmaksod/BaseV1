using FluentValidation;
using Hedaya.Application.Users.Commands.DeleteUser;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public DeleteUserCommandValidator(UserManager<AppUser> userManager)
    {
        _userManager = userManager;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.")
            .MustAsync(UserExists).WithMessage("User does not exist.");

        RuleFor(x => x.DeletedReason)
            .NotEmpty().WithMessage("A reason for the deletion is required.");
    }

    private async Task<bool> UserExists(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }
}
