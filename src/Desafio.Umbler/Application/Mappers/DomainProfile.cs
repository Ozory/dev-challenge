using AutoMapper;
using Desafio.Umbler.Models;
using Desafio.Umbler.ViewModels;

namespace Desafio.Umbler.Application.Mappers
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Domain, DomainViewModel>().ReverseMap();
        }
    }
}