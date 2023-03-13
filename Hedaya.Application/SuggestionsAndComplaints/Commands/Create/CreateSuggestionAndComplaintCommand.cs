using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.SuggestionsAndComplaints.Commands.Create
{
    public class CreateSuggestionAndComplaintCommand : IRequest<int>
    {
        public string TraineeName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }


        public class CreateSuggestionAndComplaintCommandHandler : IRequestHandler<CreateSuggestionAndComplaintCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateSuggestionAndComplaintCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateSuggestionAndComplaintCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var Entity = new SuggestionAndComplaint
                    {
                        TraineeName = request.TraineeName,
                        Phone = request.Phone,
                        Email = request.Email,
                        Subject = request.Subject,
                        Message = request.Message,
                        CreationDate = DateTime.Now
                    };

                     await _context.SuggestionAndComplaints.AddAsync(Entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Entity.Id;
                }
                catch (Exception ex )
                {

                    throw new Exception(ex.Message);
                }
               

              
            }
        }

    }
}
