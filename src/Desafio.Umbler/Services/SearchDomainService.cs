using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Umbler.Models;
using DnsClient;
using System.Text.RegularExpressions;
using Whois.NET;
using Desafio.Umbler.Infrastructure.Interfaces;

namespace Desafio.Umbler.Services
{
    public class SearchDomainService : ISearchDomainService
    {
        private readonly DatabaseContext _db;
        private readonly IUmblerWhoisClient _whoisClient;
        private readonly ILookupClient _lookupClient;

        public SearchDomainService(DatabaseContext db, IUmblerWhoisClient whoisClient, ILookupClient lookupClient)
        {
            _db = db;
            _whoisClient = whoisClient;
            _lookupClient = lookupClient;
        }

        public async Task<Domain> SearchDomain(string domainName)
        {
            var domain = await _db.Domains.FirstOrDefaultAsync(d => d.Name == domainName);
            domain = await InsertIfNew(domainName, domain);
            if (!string.IsNullOrEmpty(domain.Ip)) await _db.SaveChangesAsync();
            return domain;
        }

        private async Task<Domain> InsertIfNew(string domainName, Domain domain)
        {
            if (domain == null || DateTime.Now.Subtract(domain.UpdatedAt).TotalMinutes > domain.Ttl)
            {
                var response = await _whoisClient.QueryAsync(domainName);

                var result = await _lookupClient.QueryAsync(domainName, QueryType.A);
                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                var hostResponse = await _whoisClient.QueryAsync(ip);
                domain = AssignDomain(domain, domainName, response, record, ip, hostResponse);
            }

            return domain;
        }

        private Domain AssignDomain(
            Domain domain,
            string domainName,
            WhoisResponse response,
            DnsClient.Protocol.ARecord record,
            string ip,
            WhoisResponse hostResponse)
        {
            var itsANewDomain = false;
            if (domain == null)
            {
                domain = new Domain();
                itsANewDomain = true;
            }

            domain.Name = domainName;
            domain.Ip = ip;
            domain.UpdatedAt = DateTime.Now;
            domain.WhoIs = response.Raw;
            domain.Ttl = record?.TimeToLive ?? 0;
            domain.HostedAt = hostResponse.OrganizationName;

            if (itsANewDomain) _db.Domains.Add(domain);
            return domain;
        }

        public bool IsValidDomainName(string domainName)
        {
            if (string.IsNullOrEmpty(domainName)) return false;
            return Regex.IsMatch(domainName, @"^([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,}$");
        }
    }
}