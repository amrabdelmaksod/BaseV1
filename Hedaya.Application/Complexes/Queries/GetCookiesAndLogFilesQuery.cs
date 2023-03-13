using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetCookiesAndLogFilesQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetCookiesAndLogFilesQuery, object>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetCookiesAndLogFilesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Complex = await _context.Complexes.Select(a => new CookiesAndLogFilesDto { Cookies = a.Cookies, LogFiles = a.LogFiles }).FirstOrDefaultAsync();
                    if (Complex == null)
                    {
                        return new  { Message = "Not Found" }; ;
                    }

                    return new  { Result = Complex };

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
