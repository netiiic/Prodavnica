using AutoMapper;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ShoppingItem, ShoppingItemDto>().ReverseMap();
        }
    }
}
