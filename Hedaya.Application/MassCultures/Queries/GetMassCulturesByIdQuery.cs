using Hedaya.Application.Interfaces;
using Hedaya.Application.MassCultures.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                .Select(x => new MassCultureDetailsDto
                {
                    Id = x.Id,
                    SubCategoryId = x.SubCategoryId,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav = false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours",
                    ImageUrl = x.ImageUrl,
                    Facebook = x.Facebook,
                    Telegram = x.Telegram,
                    Twitter = x.Twitter,
                    Youtube = x.Youtube
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (culture == null)
            {
                return null;
            }

            var relatedCultures = await _context.MassCultures
                .Where(x => x.SubCategoryId == culture.SubCategoryId && x.Id != culture.Id)
                .Select(x => new MassCultureDetailsDto
                {
                    Id = x.Id,
                    SubCategoryId = x.SubCategoryId,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav = false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours",
                    ImageUrl = x.ImageUrl,
                    Facebook = x.Facebook,
                    Telegram = x.Telegram,
                    Twitter = x.Twitter,
                    Youtube = x.Youtube
                })
                .ToListAsync(cancellationToken);

            var response = new MassCultureDetailsResponse
            {
                MassCulture = culture,
                RelatedMassCultures = relatedCultures
            };

            return new { Result = response };
        }
    }

}
