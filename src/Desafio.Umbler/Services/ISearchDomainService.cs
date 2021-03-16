using System.Threading.Tasks;
using Desafio.Umbler.Models;

namespace Desafio.Umbler.Services
{
    public interface ISearchDomainService
    {
        Task<Domain> SearchDomain(string domainName);

        bool IsValidDomainName(string domainName);
    }
}