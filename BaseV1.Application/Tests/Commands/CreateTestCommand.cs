using BaseV1.Application.Interfaces;
using BaseV1.Domain;
using MediatR;


namespace BaseV1.Application.Tests.Commands
{
    public class CreateTestCommand : IRequest<int?>
    {
       
        public string Name { get; set; }
      
        public class Handler : IRequestHandler<CreateTestCommand, int?>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int?> Handle(CreateTestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var x = new TestClass { Name = request.Name };
                    
                    await _context.Tests.AddAsync(x);
                    await _context.SaveChangesAsync();
                    return x.Id;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
