using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.AboutPlatform.Queries
{
    public class GetAbouPlatformQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetAbouPlatformQuery, object>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAbouPlatformQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var PlatformVideo = (await _context.Complexes.FirstOrDefaultAsync()).AboutPlatformVideoUrl;           
                    var PlatformFeatures = await _context.PlatformFeatures.Where(a=>!a.Deleted).Select(a=>new PlatformFeaturesDto { Id = a.Id, Title = a.Title, Description = a.Description}).ToListAsync();
                    var PlatformFields = await _context.PlatformFields.Where(a=>!a.Deleted).Select(a=>new PlatformFieldDto { Id = a.Id, Title = a.Title, Description = a.Description,IconUrl = a.IconUrl}).ToListAsync();
                    var PlatformWorkAxes = await _context.PlatformWorkAxes.Where(a=>!a.Deleted).Select(a=>new PlatformWorkAxesDto { Id = a.Id, Title = a.Title, Description = a.Description}).ToListAsync();

                    var AboutPlatform = new AboutPlatformDto {  PlatformWorkAxes = PlatformWorkAxes , PlatformFeatures = PlatformFeatures, PlatformFields = PlatformFields,VideoUrl = PlatformVideo};
                   
                    return new {result = AboutPlatform} ;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
