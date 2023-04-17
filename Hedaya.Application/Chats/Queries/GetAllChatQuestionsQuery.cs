using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Chats.Queries
{
    public class GetAllChatQuestionsQuery : IRequest<object>
    {

        public class GetAllChatQuestionsQueryHandler : IRequestHandler<GetAllChatQuestionsQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetAllChatQuestionsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAllChatQuestionsQuery request, CancellationToken cancellationToken)
            {

                try
                {
           
                
                
                    var chatQuestions = await _context.ChatQuestions
                   .Select(q => new 
                   {
                       Id = q.Id,
                       Question = q.Question,

                   })
                   .ToListAsync();
                    return new { Result = chatQuestions };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
               

               
            }
        }
    }
}
