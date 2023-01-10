using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface ICountryRepository
    {
        public IEnumerable<Country> GetCountries();
        public Country GetCountry(int id);
        public ICollection<Owner> GetOwnersByCountry(int countryId);

        public Country GetCountryByOwnerID(int ownerId);
        public bool CountryExists(int countryId);
    }
}
