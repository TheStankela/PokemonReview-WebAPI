using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof (IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            return Ok(countries);
        }
        [HttpGet ("{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountry(int id)
        {
            var country = _countryRepository.GetCountry(id);
            return Ok(country);
        }
        [HttpGet("countryowner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryByOwnerID(int ownerId) 
        { 
            var country = _countryRepository.GetCountryByOwnerID(ownerId);
            return Ok(country);
        }
        [HttpGet("countryowners/{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            var country = _countryRepository.GetOwnersByCountry(countryId);
            return Ok(country);
        }
    }
}
