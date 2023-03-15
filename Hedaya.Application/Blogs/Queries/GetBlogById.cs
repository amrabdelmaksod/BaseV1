using Hedaya.Application.Blogs.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Blogs.Queries
{
    public class GetBlogById : IRequest<object>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetBlogById, object>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetBlogById request, CancellationToken cancellationToken)
            {
                try
                {
                    var blog = await _context.Blogs.Where(a=>a.Id == request.Id).Select(a => new BlogDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        ImgUrl = a.ImagePath,
                        Facebook = a.Facebook,
                        Instagram = a.Instagram,
                        Twitter = a.Twitter,
                        Whatsapp = a.Whatsapp,
                        Youtube = a.Youtube,
                    }).FirstOrDefaultAsync(cancellationToken);

                    if (blog == null)
                        return new {Message = $"There is no Blogs With This Id :{request.Id} "};


                    return new {Result = blog } ;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
