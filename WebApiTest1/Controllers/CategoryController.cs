using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest1.Dto;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories() 
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            return Ok(categories);
        }
        [HttpGet ("{id}")]
        [ProducesResponseType(200, Type =typeof(Category))]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategory(id));
            return Ok(category);
        }

        [HttpGet ("pokefilter/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategoryByPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var category = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategoryByPokemon(pokeId));

            return Ok(category);
        }
        [HttpGet("catfilter/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var pokemon = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonsByCategory(categoryId));

            return Ok(pokemon);
        }



    }
}
