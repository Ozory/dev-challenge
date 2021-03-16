using System.Threading.Tasks;
using DnsClient;

namespace Desafio.Umbler.Infrastructure.Interfaces
{
    public interface IUmblerLookupClient : ILookupClient
    {
        Task<IDnsQueryResponse> QueryAsync(string query);
    }
}