using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Infrastructure.Interfaces
{
    public interface IUmblerWhoisClient
    {
        Task<WhoisResponse> QueryAsync(string query);
    }
}