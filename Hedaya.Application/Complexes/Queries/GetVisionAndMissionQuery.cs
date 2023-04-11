using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetVisionAndMissionQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetVisionAndMissionQuery, object>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetVisionAndMissionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Complex = await _context.Complexes.Select(a => new VisionAndMissionDto 
                    { 
                        Vision = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.VisionAr : a.VisionEn,
                        Mission = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.MissionAr : a.MissionEn 
                    
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
