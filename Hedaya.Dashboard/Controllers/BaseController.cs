using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        #region Dependency Injection



        private IMediator _mediator;
        private IApplicationDbContext _ctx;
        protected IApplicationDbContext _context => _ctx ?? (_ctx = HttpContext.RequestServices.GetService<IApplicationDbContext>());
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        #endregion
    }
}
