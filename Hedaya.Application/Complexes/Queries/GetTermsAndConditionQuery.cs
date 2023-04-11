using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetTermsAndConditionQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetTermsAndConditionQuery, object>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetTermsAndConditionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Complex = await _context.Complexes.Select(a=>new TermsAndConditionDto
                    {
                        Conditions = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.ConditionsAr : a.ConditionsEn, 
                        Terms = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.TermsAr : a.TermsEn,
                    }
                    ).FirstOrDefaultAsync();
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
