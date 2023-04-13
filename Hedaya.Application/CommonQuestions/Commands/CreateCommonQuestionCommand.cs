using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.CommonQuestions.Commands
{
    public class CreateCommonQuestionCommand : IRequest<int>
    {
        public string Question { get; set; }
        public string Response { get; set; }
    }

    public class CreateCommonQuestionCommandHandler : IRequestHandler<CreateCommonQuestionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCommonQuestionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCommonQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = new CommonQuestion
            {
                Question = request.Question,
                Response = request.Response,
                CreatedById = "HedayaAdmin",
                CreationDate = DateTime.Now,
            };

            _context.CommonQuestions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
