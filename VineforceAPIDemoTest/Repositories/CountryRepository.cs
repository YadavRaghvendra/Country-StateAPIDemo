using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using VineforceAPIDemoTest.Data;
using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;

namespace VineforceAPIDemoTest.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries
                .Include(c => c.States)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country> AddCountryAsync(CreateCountryDto dto)
        {
            var country = new Country();
            country.CreatedBy = "Admin"; // or get from current user
            country.CreatedOn = DateTime.UtcNow;
            country.LastUpdatedOn = DateTime.UtcNow;
            country.CountryName = dto.CountryName;
            country.CountryCode = dto.CountryCode;
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country> UpdateCountryAsync(UpdateCountryDto dto)
        {
            var existingCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (existingCountry == null)
            {
                throw new KeyNotFoundException($"Country with ID {dto.Id} not found.");
            }
            existingCountry.LastUpdatedOn = DateTime.UtcNow;
            existingCountry.CountryName = dto.CountryName;
            existingCountry.CreatedBy = "Admin";
            existingCountry.Id= dto.Id;
            existingCountry.CountryCode = dto.CountryCode;
            await _context.SaveChangesAsync();
            return existingCountry;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
                return false;

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
