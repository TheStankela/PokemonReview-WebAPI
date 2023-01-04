using WebApiTest1.Data;
using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface IPokemonRepository
    {
        public IEnumerable<Pokemon> GetPokemons();
    }
}
