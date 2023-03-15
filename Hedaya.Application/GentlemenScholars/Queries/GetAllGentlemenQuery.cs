using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.GentlemenScholars.Queries
{
    public class GetAllGentlemenQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetAllGentlemenQuery, object>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {

                _context = context;
            }

            public async Task<object> Handle(GetAllGentlemenQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Gentlemens = await _context.GentlemenScholars

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
                       .ToListAsync();


                    return  new { Result = Gentlemens };

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
