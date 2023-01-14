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

        [HttpGet ("GetPokemonsCategory/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategoryByPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryByPokemon(pokeId));

            return Ok(category);
        }
        [HttpGet("GetPokemonsByCategoryID/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var pokemon = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonsByCategory(categoryId));

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }


            return StatusCode(200, "Successfully created.");
        }

        [HttpPut("{catId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory([FromBody]CategoryDto updatedCategory, int catId ) {

            if (updatedCategory == null)
                return BadRequest();

            if (!_categoryRepository.CategoryExists(catId))
                return NotFound("Category does not exist.");

            if (updatedCategory.Id != catId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Error occured while updating the category.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "Category updated successfully.");
        
        }

        [HttpDelete ("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryToDelete = _categoryRepository.GetCategory(categoryId);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound("Category does not exist.");

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Error while deleting category.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "Category deleted successfully.");
        }



    }
}
