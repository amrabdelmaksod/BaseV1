using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MethodologicalExplanations.Queries
{
    public class GetMethodlogicalExplanationsBySubCategoryIdQuery : IRequest<object>
    {
        public int SubCategoryId { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetMethodlogicalExplanationsBySubCategoryIdQueryHandler : IRequestHandler<GetMethodlogicalExplanationsBySubCategoryIdQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetMethodlogicalExplanationsBySubCategoryIdQueryHandler(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<object> Handle(GetMethodlogicalExplanationsBySubCategoryIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<MethodologicalExplanation> query = _context.MethodologicalExplanations.Where(x => x.SubCategoryId == request.SubCategoryId);

            int totalCount = await query.CountAsync(cancellationToken);

            var categories = await _context.SubCategories
                .Select(x => new SubCategoryDto
                {
                    Id = x.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
                    IconUrl = x.ImgIconUrl
                })
                .ToListAsync(cancellationToken);

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
                    ImageUrl =x.ImageUrl,
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
