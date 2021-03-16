using System.Threading.Tasks;
using Desafio.Umbler.Application.Queries;
using Desafio.Umbler.ViewModels;

namespace Desafio.Umbler.Application
{
    public interface ISearchDomainApplication
    {
        Task<DomainViewModel> Handle(SearchDomainQuery request);
    }
}