using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Courses.Queries
{
    public class GetForumByCourseIdQuery : IRequest<object>
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }

    public class GetForumByCourseIdQueryHandler : IRequestHandler<GetForumByCourseIdQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetForumByCourseIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetForumByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var traineeId = await _context.Trainees
                 .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                 .Select(a => a.Id)
                 .FirstOrDefaultAsync(cancellationToken);

            var forum = await _context.Forums
                  .Where(f => f.CourseId == request.CourseId)
                  .Select(f => new ForumDto
                  {
                      Id = f.Id,
                      Posts = f.Posts.Select(p => new PostDto
                      {
                          Id = p.Id,
                          TraineeName = p.Trainee.FullName,
                          Text = p.Text,
                          TraineeImage = p.Trainee.ProfilePictureImagePath ?? "",
                          PostImages = p.PostImages.Select(a=>a.ImageUrl).ToList(),
                          Coments = p.Comments.Select(c => new CommentDto
                          {
                              Id = c.Id,
                              TraineeName = c.Trainee.FullName,
                              Text = c.Text,

                          }).ToList(),
                          NumberOfLikes = _context.PostLikes.Count(a=>a.PostId == p.Id),
                          Liked = _context.PostLikes.Any(pl => pl.TraineeId == traineeId&&pl.PostId == p.Id)
                      }).ToList()
                  })
                  .FirstOrDefaultAsync(cancellationToken);

            return new { Result = forum };
        }
    }

}
