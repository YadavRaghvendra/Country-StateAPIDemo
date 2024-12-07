using Microsoft.AspNetCore.Mvc;
using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;
using VineforceAPIDemoTest.Repositories;

namespace VineforceAPIDemoTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountry()
        {
            var countries = await _countryRepository.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry([FromBody] CreateCountryDto country)
        {
            var result = await _countryRepository.AddCountryAsync(country);
            return CreatedAtAction(nameof(GetCountry), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> UpdateCountry(int id, [FromBody] UpdateCountryDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }
            var country = await _countryRepository.UpdateCountryAsync(dto);
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var deleted = await _countryRepository.DeleteCountryAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
