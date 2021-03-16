using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Desafio.Umbler.Infrastructure.Interfaces;
using DnsClient;

namespace Desafio.Umbler.Infrastructure
{
    public class UmblerLookupClient : IUmblerLookupClient, ILookupClient
    {
        public IReadOnlyCollection<NameServer> NameServers => throw new NotImplementedException();

        public LookupClientSettings Settings => throw new NotImplementedException();

        public TimeSpan? MinimumCacheTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool EnableAuditTrail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseCache { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Recursion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Retries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ThrowDnsErrors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseRandomNameServer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ContinueOnDnsError { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan Timeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseTcpFallback { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseTcpOnly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDnsQueryResponse Query(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse Query(DnsQuestion question)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse Query(DnsQuestion question, DnsQueryAndServerOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public async Task<IDnsQueryResponse> QueryAsync(string query)
        {
            return await this.QueryAsync(query, QueryType.A, default);
        }

        public Task<IDnsQueryResponse> QueryAsync(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryCache(DnsQuestion question)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryCache(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryReverse(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryReverse(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServer(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServer(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }

        public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}