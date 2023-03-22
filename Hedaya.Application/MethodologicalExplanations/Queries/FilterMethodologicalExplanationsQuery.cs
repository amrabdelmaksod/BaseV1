using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MethodologicalExplanations.Queries
{
    public class FilterMethodologicalExplanationsQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
        public int? SubCategoryId { get; set; } // optional filter by subcategory ID
        public bool SortByDurationAscending { get; set; } // true for ascending order, false for descending order
        public string searchKeyword { get; set; }
        public class FilterMethodologicalExplanationsQueryHandler : IRequestHandler<FilterMethodologicalExplanationsQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public FilterMethodologicalExplanationsQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<object> Handle(FilterMethodologicalExplanationsQuery request, CancellationToken cancellationToken)
            {
                // Filter by subcategory ID if provided
                IQueryable<MethodologicalExplanation> query = _context.MethodologicalExplanations;
                if (request.SubCategoryId.HasValue)
                {
                    query = query.Where(x => x.SubCategoryId == request.SubCategoryId.Value);
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

                // Get total count for pagination
                int totalCount = await query.CountAsync(cancellationToken);

                // Get paginated results
                int skip = (request.PageNumber - 1) * 10;
                var explanations = await query.Skip(skip)
                    .Take(10)
                    .Select(x => new MethodlogicalExplanationDto
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

                // Get categories for dropdown
                var categories = await _context.SubCategories
                    .Select(x => new SubCategoryDto
                    {
                        Id = x.Id,
                        Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
                        IconUrl = x.ImgIconUrl
                    })
                    .ToListAsync(cancellationToken);

                var response = new MethodlogicalExplanationResponse
                {
                    TotalCount = totalCount,
                    Categories = categories,
                    AllExplanations = explanations
                };

                return new { Result = response };
            }
        }
    }

}
