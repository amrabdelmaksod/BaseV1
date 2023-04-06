using System.Threading;
using System.Threading.Tasks;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Complexes.Commands.Delete
{
    public class DeleteComplexCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteComplexCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteComplexCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Complexes.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Complex));
                }

                entity.Deleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
