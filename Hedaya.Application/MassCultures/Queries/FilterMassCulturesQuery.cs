using Hedaya.Application.Interfaces;
using Hedaya.Application.MassCultures.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MassCultures.Queries
{
  
    public class FilterMassCulturesQuery : IRequest<object>
    {
        public int? SubcategoryId { get; set; } // optional filter by subcategory ID
        public bool SortByDurationAscending { get; set; } // true for ascending order, false for descending order
        public int PageNumber { get; set; }
        public string searchKeyword { get; set; }
    }

    public class FilterMassCulturesQueryHandler : IRequestHandler<FilterMassCulturesQuery, object>
    {

        private readonly IApplicationDbContext _context;

        public FilterMassCulturesQueryHandler(IApplicationDbContext dbContext)
        {

            _context = dbContext;
        }

        public async Task<object> Handle(FilterMassCulturesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<MassCulture> query = _context.MassCultures;

            // Apply subcategory filter
            if (request.SubcategoryId.HasValue)
            {
                query = query.Where(x => x.SubCategoryId == request.SubcategoryId.Value);
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(request.searchKeyword))
            {
                query = query.Where(x => x.TitleEn.Contains(request.searchKeyword) || x.TitleAr.Contains(request.searchKeyword) || x.Description.Contains(request.searchKeyword));
            }

            // Sort by duration
            if (request.SortByDurationAscending)
            {
                query = query.OrderBy(x => x.Duration);
            }
            else
            {
                query = query.OrderByDescending(x => x.Duration);
            }

            // Get categories
            var categories = await _context.SubCategories
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
                    IconUrl = x.ImgIconUrl
                })
                .ToListAsync(cancellationToken);

            // Get total count of results
            int totalCount = await query.CountAsync(cancellationToken);

            // Paginate results
            int skip = (request.PageNumber - 1) * 10;
            var massCultures = await query.Skip(skip)
                .Take(10)
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

            // Create response object
            var response = new MassCulturesResponse
            {
                TotalCount = totalCount,
                Categories = categories,
                AllCultures = massCultures
            };

            return new { Result = response };
        }

    }
}
