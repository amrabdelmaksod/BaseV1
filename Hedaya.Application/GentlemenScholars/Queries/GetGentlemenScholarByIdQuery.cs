using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.GentlemenScholars.Queries
{
    public class GetGentlemenScholarByIdQuery : IRequest<object>
    {
        public string Id { get; set; }
        public class Handler : IRequestHandler<GetGentlemenScholarByIdQuery, object>
        {
            public string Id { get; set; }
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {

                _context = context;
            }

            public async Task<object> Handle(GetGentlemenScholarByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Gentlemens = await _context.GentlemenScholars.Where(a=>a.Id == request.Id)

                        .Select(a => new GentlemenScholarDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Facebook = a.Facebook,
                            Title = a.Title,
                            Twitter = a.Twitter,
                            Youtube = a.Youtube
                        })
                       .FirstOrDefaultAsync(cancellationToken);

                    return new { Result = Gentlemens }  ;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
