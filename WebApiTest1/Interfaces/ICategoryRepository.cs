using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface ICategoryRepository
    {
        public Category GetCategory(int id);
        public ICollection<Category> GetCategories();
        public Category GetCategoryByPokemon(int id);
        
        public ICollection<Pokemon> GetPokemonsByCategory(int id);
        public bool CategoryExists (int id);
        public bool CreateCategory(Category category);
        public bool Save();
    }
}
