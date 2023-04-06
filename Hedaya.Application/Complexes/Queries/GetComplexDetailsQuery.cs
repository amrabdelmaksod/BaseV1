using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Complexes.Queries.GetComplex
{
    public class GetComplexQuery : IRequest<ComplexDetailsDto>
    {
    }

    public class GetComplexQueryHandler : IRequestHandler<GetComplexQuery, ComplexDetailsDto>
    {
        private readonly IApplicationDbContext _context;

        public GetComplexQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ComplexDetailsDto> Handle(GetComplexQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complexes
                .Select(c => new ComplexDetailsDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    AddressDescription = c.AddressDescription,
                    Email = c.Email,
                    Mobile = c.Mobile,
                    LandlinePhone = c.LandlinePhone,
                    Terms = c.Terms,
                    Conditions = c.Conditions,
                    LogFiles = c.LogFiles,
                    Cookies = c.Cookies,
                    Vision = c.Vision,
                    Mission = c.Mission,
                    AboutPlatformVideoUrl = c.AboutPlatformVideoUrl
                })
                .FirstOrDefaultAsync(cancellationToken);

            return entity;
        }
    }
}
