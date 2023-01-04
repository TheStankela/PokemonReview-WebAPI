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
        public IEnumerable<Pokemon> GetPokemons()
        {
            return _dataContext.POkemons.OrderBy(p => p.Id).ToList();
        }
    }
}
