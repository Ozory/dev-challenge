
using Desafio.Umbler.ViewModels;

namespace Desafio.Umbler.Application.Queries
{
    public class SearchDomainQuery
    {
        public string Domain { get; set; }
        public SearchDomainQuery(string SearchDomain)
        {
            Domain = SearchDomain;
        }

    }
}