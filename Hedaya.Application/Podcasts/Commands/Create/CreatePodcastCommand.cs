using MediatR;

namespace Hedaya.Application.Podcasts.Commands.Create
{
    public class CreatePodcastCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
