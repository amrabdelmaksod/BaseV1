using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.CommonQuestions.Commands
{
    public class UpdateCommonQuestionCommand : IRequest
    {
        public int? Id { get; set; }
        public string? Question { get; set; }
        public string? Response { get; set; }
    }

    public class UpdateCommonQuestionCommandHandler : IRequestHandler<UpdateCommonQuestionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCommonQuestionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommonQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CommonQuestions.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CommonQuestion));
            }

            entity.Question = request.Question??entity.Question;
            entity.Response = request.Response ?? entity.Response;
            entity.ModifiedById = "HedayaAdmin";
            entity.ModificationDate = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
