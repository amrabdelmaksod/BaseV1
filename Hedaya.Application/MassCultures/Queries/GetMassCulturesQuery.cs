using Hedaya.Application.Interfaces;
using Hedaya.Application.MassCultures.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MassCultures.Queries
{
    public class GetMassCulturesQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
    }

    public class GetMassCulturesQueryHandler : IRequestHandler<GetMassCulturesQuery, object>
    {
     
        private readonly IApplicationDbContext _context;

        public GetMassCulturesQueryHandler( IApplicationDbContext dbContext)
        {
           
            _context = dbContext;
        }

        public async Task<object> Handle(GetMassCulturesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.SubCategories
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ?  x.NameAr : x.NameEn,
                    IconUrl = x.ImgIconUrl
                })
                .ToListAsync(cancellationToken);

            var totalCount = await _context.MassCultures.CountAsync(cancellationToken);

            int skip = (request.PageNumber - 1) * 10;

            var massCultures = await _context.MassCultures.Skip(skip)
                        .Take(10)
                .Select(x => new MassCultureDto
                {
                    Id = x.Id,
                    Title =CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ?  x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav =false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours",
                    SubCategoryId = x.SubCategoryId,
                    ImageUrl = x.ImageUrl,
         
                })
                .ToListAsync(cancellationToken);

            var response = new MassCulturesResponse
            {
                TotalCount = totalCount,
                Categories = categories,
                AllCultures = massCultures
            };

            return new {Result = response } ;
        }
    }



}
