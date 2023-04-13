using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Users.Commands.ChangeStatus
{
    public class ChangeUserStatusCommand : IRequest
    {
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand>
        {
            private readonly IApplicationDbContext _context;

            public ChangeUserStatusCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.AppUsers.FindAsync(request.UserId);

                if (user == null)
                {
                    throw new NotFoundException(nameof(AppUser));
                }

                user.IsActive = request.IsActive;
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
