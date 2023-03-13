using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetComplexDataQuery : IRequest<object>
    {
        public class Handler : IRequestHandler<GetComplexDataQuery, object>
        {

            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetComplexDataQuery request, CancellationToken cancellationToken)
            {
                try
                {
                  


                    var Complex = await _context.Complexes.Select(a => new ComplexDto { Title = a.Title, Email = a.Email,AddressDescription = a.AddressDescription,LandlinePhone = a.LandlinePhone, Mobile = a.Mobile }).FirstOrDefaultAsync();
                    if (Complex == null)
                    {
                        return new  { Message = "Not Found" }; ;
                    }

                    return new  {Result = Complex };

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
