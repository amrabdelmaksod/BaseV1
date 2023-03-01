using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetComplexDataQuery : IRequest<ComplexDto>
    {
        public class Handler : IRequestHandler<GetComplexDataQuery, ComplexDto>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ComplexDto> Handle(GetComplexDataQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var Complex = await _context.Complexes.Select(a=>new ComplexDto { Conditions = a.Conditions, Terms = a.Terms}).FirstOrDefaultAsync();
                    if (Complex == null)
                    {
                        return null;
                    }

                    //var data = await PagedList<UsersLiDto>.CreateAsync(users.AsNoTracking(), request.userParams.PageNumber, request.userParams.PageSize);

                    return Complex;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
