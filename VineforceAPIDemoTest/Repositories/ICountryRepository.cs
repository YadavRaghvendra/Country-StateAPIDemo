using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;

namespace VineforceAPIDemoTest.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdAsync(int id);
        Task<Country> AddCountryAsync(CreateCountryDto dto);
        Task<Country> UpdateCountryAsync(UpdateCountryDto dto);
        Task<bool> DeleteCountryAsync(int id);
    }
}
