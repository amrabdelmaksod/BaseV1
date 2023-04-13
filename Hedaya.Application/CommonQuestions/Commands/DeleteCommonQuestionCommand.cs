using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.CommonQuestions.Commands
{
    public class DeleteCommonQuestionCommand : IRequest
    {
        public int Id { get; set; }
        public class DeleteCommonQuestionCommandHandler : IRequestHandler<DeleteCommonQuestionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCommonQuestionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCommonQuestionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.CommonQuestions.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(CommonQuestion));
                }

                entity.Deleted = true;
                entity.ModificationDate = DateTime.Now;
                entity.ModifiedById = "HedayaAdmin";
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
