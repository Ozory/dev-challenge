using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Application.Queries;
using Desafio.Umbler.Application;

namespace Desafio.Umbler.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        private readonly ISearchDomainApplication _searchApplication;

        public DomainController(ISearchDomainApplication searchApplication)
        {
            _searchApplication = searchApplication;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            var result = await _searchApplication.Handle(new SearchDomainQuery(domainName));
            return Ok(result);
        }
    }
}
