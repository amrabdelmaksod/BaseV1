using Hedaya.Application.Blogs.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Blogs.Queries
{
    public class GetAllBlogsQuery : IRequest<object>
    {
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetAllBlogsQuery,object>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
             {
                try
                {
                    var PageSize = 10;

                    int totalCount = await _context.Blogs.CountAsync(cancellationToken);

                    int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);


                    int skip = (request.PageNumber - 1) * PageSize;

                    var blogs = await _context.Blogs
                        .OrderByDescending(b => b.Id)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(b => new BlogDto
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Description = b.Description,
                            ImgUrl = b.ImagePath,
                            Facebook = b.Facebook,
                            Instagram = b.Instagram,
                            Twitter = b.Twitter,
                            Whatsapp = b.Whatsapp,
                            Youtube = b.Youtube
                        })
                        .ToListAsync(cancellationToken);


                 var AllBlogs = new PaginatedList<BlogDto>(blogs, totalCount, request.PageNumber, PageSize, totalPages);


                    return new { Result = AllBlogs } ;
       
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
