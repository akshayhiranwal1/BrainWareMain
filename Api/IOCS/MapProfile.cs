using Api.Infrastructure.Entities;
using Api.Models;
using AutoMapper;

namespace Api.IOCS
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Orderproduct, OrderProductViewModel>().ReverseMap();
        }
	}
}

