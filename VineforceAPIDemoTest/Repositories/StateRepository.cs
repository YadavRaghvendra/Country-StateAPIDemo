using Microsoft.EntityFrameworkCore;
using VineforceAPIDemoTest.Data;
using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;

namespace VineforceAPIDemoTest.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<State>> GetAllStatesAsync()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<State> GetStateByIdAsync(int id)
        {
            return await _context.States.Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<State?> GetStateByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("State name cannot be null or empty.", nameof(name));
            }

            return await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.StateName.ToLower() == name.ToLower());
        }


        public async Task<State> CreateStateAsync(CreateStateDto dto)
        {
            var s = new State();
            s.CreatedBy = "user";
            s.CreatedOn = DateTime.UtcNow;
            s.LastUpdatedOn = DateTime.UtcNow;
            s.CountryId = dto.CountryId;
            s.StateName = dto.StateName;
            s.StateCode = dto.StateCode;

            _context.States.Add(s);
            await _context.SaveChangesAsync();
            return s;

        }
        public async Task<State> UpdateStateAsync(UpdateStateDto dto)
        {
            var existingState = await _context.States.FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (existingState == null)
            {
                throw new KeyNotFoundException($"State with ID {dto.Id} not found.");
            }
            existingState.StateName = dto.StateName;
            existingState.StateCode = dto.StateCode;
            existingState.CountryId = dto.CountryId;
            existingState.LastUpdatedOn = DateTime.UtcNow;
            existingState.CreatedBy = "user";

            await _context.SaveChangesAsync();

            return existingState;
        }


        public async Task<bool> DeleteStateAsync(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
                return false;

            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
