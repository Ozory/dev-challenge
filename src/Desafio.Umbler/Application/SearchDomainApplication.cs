using System.Threading.Tasks;
using AutoMapper;
using Desafio.Umbler.Application.Queries;
using Desafio.Umbler.Services;
using Desafio.Umbler.ViewModels;

namespace Desafio.Umbler.Application
{
    public class SearchDomainApplication : ISearchDomainApplication
    {
        private readonly ISearchDomainService _domainSearchService;
        private readonly IMapper _mapper;
        public SearchDomainApplication(ISearchDomainService domainSearchService, IMapper mapper)
        {
            _domainSearchService = domainSearchService;
            _mapper = mapper;
        }

        public async Task<DomainViewModel> Handle(SearchDomainQuery request)
        {
            var isValidDomain = _domainSearchService.IsValidDomainName(request.Domain);
            if (!isValidDomain) return new DomainViewModel() { Valid = false };

            var domain = await _domainSearchService.SearchDomain(request.Domain);
            var dvModel = _mapper.Map<DomainViewModel>(domain);
            dvModel.Valid = dvModel.Id > 0;
            return dvModel;
        }
    }
}