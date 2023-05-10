using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.SuggestionsAndComplaints.Commands.Create
{
    public class CreateSuggestionAndComplaintCommand : IRequest<object>
    {
        public string TraineeName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }


        public class CreateSuggestionAndComplaintCommandHandler : IRequestHandler<CreateSuggestionAndComplaintCommand, object>
        {
            private readonly IApplicationDbContext _context;

            public CreateSuggestionAndComplaintCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(CreateSuggestionAndComplaintCommand request, CancellationToken cancellationToken)
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
                    return new {ItemId = Entity.Id };
                }
                catch (Exception ex )
                {

                    throw new Exception(ex.Message);
                }
               

              
            }
        }

    }
}
