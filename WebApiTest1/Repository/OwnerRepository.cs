using AutoMapper;
using WebApiTest1.Data;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _dataContext;

        public OwnerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Owner> GetOwners()
        {
            return _dataContext.Owners.ToList();
        }

        public Owner GetOwners(int id)
        {
            return _dataContext.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _dataContext.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _dataContext.Owners.Any(o => o.Id == id);
        }
    }
}
