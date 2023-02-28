using Hedaya.Application.Blogs.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Blogs.Queries
{
    public class GetAllBlogsQuery : IRequest<IEnumerable<BlogDto>>
    {
      

        public class Handler : IRequestHandler<GetAllBlogsQuery, IEnumerable<BlogDto>>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<BlogDto>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
             {
                try
                {
                    var x = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                  var blogs = await _context.Blogs.Select(a=>new BlogDto
                  { 
                      Id = a.Id,
                      Title = a.Title,                   
                      Description= a.Description,
                      ImgUrl = a.ImagePath,
                      Facebook = a.Facebook,
                      Instagram = a.Instagram,
                      Twitter = a.Twitter,
                      Whatsapp = a.Whatsapp,
                      Youtube = a.Youtube, 
                  }).ToListAsync(cancellationToken);
                    return blogs;
       
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
