using Hedaya.Application.Interfaces;
using Hedaya.Application.MassCultures.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MassCultures.Queries
{
    public class GetMassCulturesBySubcategoryIdQuery : IRequest<object>
    {
        public int SubcategoryId { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetMassCulturesBySubcategoryIdQueryHandler : IRequestHandler<GetMassCulturesBySubcategoryIdQuery,object>
    {

        private readonly IApplicationDbContext _context;

        public GetMassCulturesBySubcategoryIdQueryHandler(IApplicationDbContext dbContext)
        {
          
            _context = dbContext;
        }

        public async Task<object> Handle(GetMassCulturesBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.SubCategories
           .Select(x => new CategoryDto
           {
               Id = x.Id,
               Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
               IconUrl = x.ImgIconUrl
           })
           .ToListAsync(cancellationToken);

            int skip = (request.PageNumber - 1) * 10;


            var massCultures = await _context.MassCultures.Skip(skip)
                        .Take(10)
                .Where(x => x.SubCategoryId == request.SubcategoryId)
                .Select(x => new MassCultureDto
                {
                    Id = x.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.TitleAr : x.TitleEn,
                    Description = x.Description,
                    IsFav = false,
                    Duration = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{x.Duration} ساعات" : $"{x.Duration} hours",
                    SubCategoryId = x.SubCategoryId,
                    ImageUrl = x.ImageUrl,
                  
                })
                .ToListAsync(cancellationToken);

            var response = new MassCulturesResponse
            {
                Categories = categories,
                AllCultures = massCultures
            };

            return new { Result = response } ;
        }
    }

}
