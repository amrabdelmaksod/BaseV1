using BaseV1.Application.Interfaces;
using BaseV1.Common;
using BaseV1.Domain;
using MediatR;


namespace BaseV1.Application.Tests.Commands
{
    public class CreateTestCommand : IRequest<Response>
    {

        public string Name { get; set; }
      
        public class Handler : IRequestHandler<CreateTestCommand, Response>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(CreateTestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var x = new TestClass { Name = request.Name };
                    await _context.TestClasses.AddAsync(x);
                    await _context.SaveChangesAsync();
                    return new Response { Message = "Done", Result = new { x.Id} };
                }
                catch (Exception ex)
                {
                     
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
