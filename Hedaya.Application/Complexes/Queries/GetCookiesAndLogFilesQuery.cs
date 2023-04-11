using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                    var Complex = await _context.Complexes.Select(a => new CookiesAndLogFilesDto 
                    { Cookies = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.CookiesAr : a.CookiesEn,
                        LogFiles = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.LogFilesAr : a.LogFilesEn,
                    }).FirstOrDefaultAsync();
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
