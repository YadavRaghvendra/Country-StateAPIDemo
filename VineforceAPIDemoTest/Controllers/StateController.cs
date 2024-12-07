using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VineforceAPIDemoTest.DTOs;
using VineforceAPIDemoTest.Models;
using VineforceAPIDemoTest.Repositories;

namespace VineforceAPIDemoTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            var states = await _stateRepository.GetAllStatesAsync();          
            return Ok(states);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _stateRepository.GetStateByIdAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpPost]
        public async Task<ActionResult<State>> CreateState([FromBody] CreateStateDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.StateName))
                    return BadRequest(new { message = "Invalid state data provided." });

                var existingState = await _stateRepository.GetStateByNameAsync(dto.StateName);
                if (existingState != null && existingState.CountryId == dto.CountryId)
                    return Conflict(new { message = $"A state with the name '{dto.StateName}' already exists." });

                var createdState = await _stateRepository.CreateStateAsync(dto);

                return CreatedAtAction(nameof(GetState), new { id = createdState.Id }, createdState);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred. Please try again later." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<State>> UpdateState(int id, [FromBody] UpdateStateDto dto)
        {
            try
            {
                var updatedState = await _stateRepository.UpdateStateAsync(dto);
                if (updatedState == null)
                    return NotFound(new { message = "State not found or could not be updated." });

                return Ok(updatedState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var deleted = await _stateRepository.DeleteStateAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
