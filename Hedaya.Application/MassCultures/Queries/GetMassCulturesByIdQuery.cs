using Hedaya.Application.Interfaces;
using Hedaya.Application.MassCultures.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.Globalization;

namespace Hedaya.Application.MassCultures.Queries
{
    public class GetMassCultureByIdQuery : IRequest<object>
    {
        public int MassCultureId { get; set; }
    }

    public class GetMassCultureByIdQueryHandler : IRequestHandler<GetMassCultureByIdQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetMassCultureByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<object> Handle(GetMassCultureByIdQuery request, CancellationToken cancellationToken)
        {
            var culture = await _context.MassCultures
                .Where(x => x.Id == request.MassCultureId)
                .Select(x => new MassCultureDto
                {
                    Id = x.Id,
                    SubCategoryId = x.SubCategoryId,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav = false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours",
                    
                })
                .FirstOrDefaultAsync(cancellationToken);

             if (culture == null)
    {
                return null;
    }

            var relatedCultures = await _context.MassCultures
                .Where(x => x.SubCategoryId == culture.SubCategoryId && x.Id != culture.Id)
                .Select(x => new MassCultureDto
                {
                    Id = x.Id,
                    SubCategoryId = x.SubCategoryId,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav = false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours"
                })
                .ToListAsync(cancellationToken);

            var response = new MassCultureResponse
            {
                MassCulture = culture,
                RelatedMassCultures = relatedCultures
            };

            return new { Response = response };
        }
    }

}
