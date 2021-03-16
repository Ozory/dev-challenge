using System.Threading.Tasks;
using Desafio.Umbler.Infrastructure.Interfaces;
using Whois.NET;

namespace Desafio.Umbler.Infrastructure
{
    public class UmblerWhoisClient : IUmblerWhoisClient
    {
        public async Task<WhoisResponse> QueryAsync(string query)
        {
            return await WhoisClient.QueryAsync(query);
        }
    }
}