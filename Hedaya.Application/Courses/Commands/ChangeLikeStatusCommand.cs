using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.Courses.Commands
{
    public class ChangeLikeStatusCommand : IRequest<object>
    {
        public int PostId { get; set; }
        public string TraineeId { get; set; }
        public bool IsLike { get; set; }

        public class ChangeLikeStatusCommandHandler : IRequestHandler<ChangeLikeStatusCommand, object>
        {
            private readonly IApplicationDbContext _context;

            public ChangeLikeStatusCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(ChangeLikeStatusCommand request, CancellationToken cancellationToken)
            {
                var postLike = await _context.PostLikes.FindAsync(request.TraineeId, request.PostId);

                if (postLike != null)
                {
                    if (!request.IsLike)
                    {
                        _context.PostLikes.Remove(postLike);
                    }
                }
                else
                {
                    if (request.IsLike)
                    {
                        postLike = new PostLike
                        {
                            TraineeId = request.TraineeId,
                            PostId = request.PostId
                        };
                        await _context.PostLikes.AddAsync(postLike);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return postLike.PostId;
            }
        }
    }
}