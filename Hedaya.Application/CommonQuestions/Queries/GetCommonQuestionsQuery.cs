using Hedaya.Application.CommonQuestions.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.CommonQuestions.Queries
{
    public class GetCommonQuestionsQuery : IRequest<List<CommonQuestionDto>>
    {
    }

    public class GetCommonQuestionsQueryHandler : IRequestHandler<GetCommonQuestionsQuery, List<CommonQuestionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCommonQuestionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommonQuestionDto>> Handle(GetCommonQuestionsQuery request, CancellationToken cancellationToken)
        {


            var commonQuestions = await _context.Set<CommonQuestion>()
                .Select(q => new CommonQuestionDto
                {
                    Id = q.Id,
                    Question = q.Question,
                    Response = q.Response
                })
                .ToListAsync();

            return commonQuestions;
        }
    }

}
