using Hedaya.Application.Blogs.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Blogs.Queries
{
    public class GetBlogById : IRequest<BlogDto>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetBlogById, BlogDto>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<BlogDto> Handle(GetBlogById request, CancellationToken cancellationToken)
            {
                try
                {
                    var blog = await _context.Blogs.Where(a=>a.Id == request.Id).Select(a => new BlogDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        ImgUrl = a.ImagePath,
                    }).FirstOrDefaultAsync(cancellationToken);

                    if (blog == null)
                        return null;
                    return blog;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
