using Hedaya.Application.Blogs.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Blogs.Queries
{
    public class GetAllBlogsQuery : IRequest<object>
    {
        public int PageNumber { get; set; } = 1;

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

                    #region Add Dummy Data
                    //    List<Blog> blogsListDummy = Enumerable.Range(1,100)
                    //.Select(i => new Blog
                    //{

                    //    Title = $"عنوان المدونة {i}",
                    //    Description = $"وصف المدونة {i} هو عبارة عن مقال يتحدث عن العديد من المواضيع المختلفة، ويشمل هذا المقال أفكارًا وأفكارًا جديدة ومختلفة في عالم {i} المعرفة.",
                    //    ImagePath = $"path/to/image{i}.jpg",
                    //    Facebook = $"https://www.facebook.com/blog{i}",
                    //    Twitter = $"https://www.twitter.com/blog{i}",
                    //    Youtube = $"https://www.youtube.com/blog{i}",
                    //    Instagram = $"https://www.instagram.com/blog{i}",
                    //    Whatsapp = $"https://www.whatsapp.com/blog{i}",
                    //    Deleted = false
                    //})
                    //.ToList();
                    //    await _context.Blogs.AddRangeAsync(blogsListDummy);
                    //    await _context.SaveChangesAsync();

                    // You can also add objects dynamically using a loop or by reading from a data source such as a database
                    #endregion




                    int skip = (request.PageNumber - 1) * 10;

                    var blogs = await _context.Blogs
                        .Skip(skip)
                        .Take(10)
                        .Select(a => new BlogDto
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
                        })
                        .ToListAsync(cancellationToken);

                  
                    return new { Result = blogs} ;
       
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
