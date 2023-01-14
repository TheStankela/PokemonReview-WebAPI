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

        public bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId)
        {
            var pokemonOwnerEntity = _dataContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _dataContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            _dataContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _dataContext.Add(pokemonCategory);

            _dataContext.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            var pokemonOwnerEntity = _dataContext.PokemonOwners.Where(po => po.PokemonId == pokemon.Id).FirstOrDefault();
            var pokemonCategoriesEntity = _dataContext.PokemonCategories.Where(pc => pc.PokemonId == pokemon.Id).FirstOrDefault();
            if (pokemonOwnerEntity != null)
            {
                _dataContext.Remove(pokemonOwnerEntity);
            }

            if (pokemonCategoriesEntity != null)
            {
                _dataContext.Remove(pokemonCategoriesEntity);
            }
            
            _dataContext.Remove(pokemon);

            return Save();
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

        public ICollection<Pokemon> GetPokemons()
        {
            return _dataContext.POkemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _dataContext.POkemons.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon, int catId, int ownerId)
        {
            var pokemonOwnersEntity = _dataContext.PokemonOwners.Where(p => p.PokemonId == pokemon.Id).FirstOrDefault();
            var pokemonCategoriesEntity = _dataContext.PokemonCategories.Where(p => p.PokemonId == pokemon.Id).FirstOrDefault();
            var pokemonsOwner = _dataContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var pokemonsCategory = _dataContext.Categories.Where(c => c.Id == catId).FirstOrDefault();

            if (pokemonCategoriesEntity != null && pokemonsCategory != null)
            { 
                _dataContext.Remove(pokemonCategoriesEntity);
            }
            if (pokemonOwnersEntity != null && pokemonsOwner != null)
            {
                _dataContext.Remove(pokemonOwnersEntity);
            }

            var pokemonCategory = new PokemonCategory()
            {
                Pokemon = pokemon,
                Category = pokemonsCategory
            };

            _dataContext.Add(pokemonCategory);

            var pokemonOwner = new PokemonOwner()
            {
                Pokemon = pokemon,
                Owner = pokemonsOwner
            };
            _dataContext.Add(pokemonOwner);

            _dataContext.Update(pokemon);

            return Save();

        }
    }
}
