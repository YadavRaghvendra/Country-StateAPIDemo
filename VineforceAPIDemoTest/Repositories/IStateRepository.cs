using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;

namespace VineforceAPIDemoTest.Repositories
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllStatesAsync();
        Task<State> GetStateByIdAsync(int id);
        Task<State> GetStateByNameAsync(string name);
        Task<State> CreateStateAsync(CreateStateDto state);
        Task<State> UpdateStateAsync(UpdateStateDto state);
        Task<bool> DeleteStateAsync(int id);
    }
}
