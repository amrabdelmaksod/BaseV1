using Hedaya.Application.CommonQuestions.Models;
using Hedaya.Application.Interfaces;
using MediatR;

namespace Hedaya.Application.CommonQuestions.Queries
{
    public class GetCommonQuestionByIdQuery : IRequest<CommonQuestionDto>
    {
        public int Id { get; set; }

        public class GetCommonQuestionByIdQueryHandler : IRequestHandler<GetCommonQuestionByIdQuery, CommonQuestionDto>
        {
            private readonly IApplicationDbContext _dbContext;

            public GetCommonQuestionByIdQueryHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<CommonQuestionDto> Handle(GetCommonQuestionByIdQuery request, CancellationToken cancellationToken)
            {
                var commonQuestion = await _dbContext.CommonQuestions.FindAsync(request.Id);

                if (commonQuestion == null)
                {
                    return null;
                }

                return new CommonQuestionDto
                {
                    Id = commonQuestion.Id,
                    Question = commonQuestion.Question,
                    Response = commonQuestion.Response
                };
            }
        }
    }
}
