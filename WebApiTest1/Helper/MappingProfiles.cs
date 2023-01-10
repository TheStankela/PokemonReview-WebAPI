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
            CreateMap<Country, CountryDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Review, ReviewDto>();

        }
    }
}
