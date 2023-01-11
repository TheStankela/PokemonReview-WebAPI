using WebApiTest1.Data;
using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons();
        public Pokemon GetPokemon(int id);
        public Pokemon GetPokemon(string name);
        public decimal GetPokemonRating(int id);
        bool PokemonExists(int id);
        bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId);
        bool Save();
    }
}
