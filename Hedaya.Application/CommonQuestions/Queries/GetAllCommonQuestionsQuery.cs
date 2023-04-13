using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Application.CommonQuestions.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.CommonQuestions.Queries
{
    public class GetAllCommonQuestionsQuery : IRequest<PaginatedList<CommonQuestionDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetAllCommonQuestionsQueryHandler : IRequestHandler<GetAllCommonQuestionsQuery, PaginatedList<CommonQuestionDto>>
        {
            private readonly IApplicationDbContext _dbContext;

            public GetAllCommonQuestionsQueryHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<PaginatedList<CommonQuestionDto>> Handle(GetAllCommonQuestionsQuery request, CancellationToken cancellationToken)
            {
                // Calculate the number of items to skip based on the page number and page size
                var itemsToSkip = (request.PageNumber - 1) * request.PageSize;

                // Get the total number of items in the database
                var totalCount = await _dbContext.CommonQuestions.CountAsync();

                // Get a page of items from the database
                var items = await _dbContext.CommonQuestions
                    .OrderBy(x => x.Id)
                    .Skip(itemsToSkip)
                    .Take(request.PageSize)
                    .ToListAsync();

                // Map the items to DTOs manually
                var dtos = items.Select(x => new CommonQuestionDto
                {
                    Id = x.Id,
                    Question = x.Question,
                    Response = x.Response
                }).ToList();

                // Calculate the total number of pages based on the total count and page size
                var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

                // Create and return the paginated list
                return new PaginatedList<CommonQuestionDto>(dtos, totalCount, request.PageNumber, request.PageSize, totalPages);
            }
        }

    }
}
