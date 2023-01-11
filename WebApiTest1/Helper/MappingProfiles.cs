using AutoMapper;
using WebApiTest1.Dto;
using WebApiTest1.Models;

namespace WebApiTest1.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Pokemon, PokemonDto>().ReverseMap();
            CreateMap<Country, CountryDto>();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Reviewer, ReviewerDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Review, ReviewDto>();
            CreateMap<Review, ReviewDto>().ReverseMap();

        }
    }
}
