using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.MethodologicalExplanations.Queries
{
    public class GetAllMethodlogicalExplanationQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
        public class GetAllMethodlogicalExplanationQueryHandler : IRequestHandler<GetAllMethodlogicalExplanationQuery, object>
        {

            private readonly IApplicationDbContext _context;

            public GetAllMethodlogicalExplanationQueryHandler(IApplicationDbContext dbContext)
            {

                _context = dbContext;
            }

            public async Task<object> Handle(GetAllMethodlogicalExplanationQuery request, CancellationToken cancellationToken)
            {

                IQueryable<MethodologicalExplanation> query = _context.MethodologicalExplanations;
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

                var explanations = await _context.MethodologicalExplanations.Skip(skip)
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
