using WebApiTest1.Data;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _dataContext;

        public PokemonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Pokemon GetPokemon(int id)
        {
            return _dataContext.POkemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _dataContext.POkemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = _dataContext.Reviews.Where(p => p.Pokemon.Id == id);
            if (review.Count() <= 0)
                return 0;
            
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public IEnumerable<Pokemon> GetPokemons()
        {
            return _dataContext.POkemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _dataContext.POkemons.Any(p => p.Id == id);
        }
       
    }
}
