using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{


    public class GetFilterOptionsQuery : IRequest<object>
        {
            public class GetFilterOptionsQueryHandler : IRequestHandler<GetFilterOptionsQuery, object>
            {
                private readonly IApplicationDbContext _context;

                public GetFilterOptionsQueryHandler(IApplicationDbContext context)
                {
                    _context = context;
                }

                public async Task<object> Handle(GetFilterOptionsQuery request, CancellationToken cancellationToken)
                {
                    var categoryOptions = await _context.SubCategories
                        .Select(c => new { Id = c.Id, Name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.NameAr : c.NameEn })
                        .ToListAsync(cancellationToken);

                    //var durationOptions = await _context.Courses
                    //    .Select(c => new { Id = c.Duration.Ticks, Name = c.Duration.ToString(@"hh\:mm\:ss") })
                    //    .Distinct()
                    //    .ToListAsync(cancellationToken);

                var allFilters = new { categories = categoryOptions/*, durations = durationOptions*/ };
                    return new {result = allFilters } ;
                }
            }
        }

    
}
